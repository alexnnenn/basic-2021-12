using Assets.Scripts.Common;
using Assets.Scripts.Components;
using Assets.Scripts.Logic;
using Assets.Scripts.Managers;
using Assets.Scripts.Menus;
using Assets.Scripts.ScriptableObjectsProtos;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    internal sealed class BattleOrchestrator : MonoBehaviour
    {
        [SerializeField]
        private string _nextLevel;
        [SerializeReference]
        private Character[] LeftTeam;
        [SerializeReference]
        private Character[] RightTeam;
        [SerializeReference]
        private GameSettings Settings;
        private CharacterSelectionSystem _selector;
        private RoundNumberComponent _ui;
        private SkillsPanelComponent _skills;

        private BattleLogic _logic;
        private Team _leftTeam;
        private Team _rightTeam;
        private Guid _firstTeamId;
        private PlayerTargetChooser _playerCtr;

        private void Awake()
        {
            AudioManager.Instance.Play(AudioSourceType.Background);
            _selector = GetComponent<CharacterSelectionSystem>();
            _playerCtr = GetComponent<PlayerTargetChooser>();
            _ui = GetComponentInChildren<RoundNumberComponent>();
            _skills = GetComponentInChildren<SkillsPanelComponent>();
            _logic = new BattleLogic();
            _leftTeam = _logic.CreateTeam(CreateController(Settings.LeftControlledBy));
            foreach (var member in LeftTeam)
                _leftTeam.AddMember(member);

            _rightTeam = _logic.CreateTeam(CreateController(Settings.RightControlledBy));
            foreach (var member in RightTeam)
                _rightTeam.AddMember(member);

            if (!Settings.IsLeftCommandFirst)
                _logic.SetTeamOrder(_rightTeam, 0);

            _firstTeamId = Settings.IsLeftCommandFirst ? _leftTeam.Id : _rightTeam.Id;
        }

        private ITargetChooser CreateController(ControlledBy controllerType)
        {
            return controllerType switch
            {
                ControlledBy.Computer => new ComputerTargetChooser(),
                ControlledBy.Player => _playerCtr,
                _ => throw new InvalidOperationException("Неизвестный тип контролера")
            };
        }

        private void Start()
        {
            ResetGame();
        }

        private void OnDestroy()
        {
            AudioManager.Instance.Stop(AudioSourceType.Background);
        }

        private IEnumerator Battle()
        {
            var roundNumber = 1;
            var previousTeamId = Guid.Empty;
            foreach (var actor in _logic.EnumerateActors())
            {
                _skills.SetForActor(actor);
                var isFirstTeamMove = actor.Team.Id == _firstTeamId && previousTeamId != _firstTeamId;
                if (isFirstTeamMove)
                    yield return _ui.ShowRound(roundNumber++);
                previousTeamId = actor.Team.Id;

                IActor target = null;
                IWeapon weapon = null;
                bool actorDecided = false;
                _logic.GetAction(actor, (t, w) =>
                {
                    weapon = w;
                    target = t;
                    actorDecided = true;
                });
                yield return new WaitUntil(() => actorDecided);
                if (weapon == null)
                    Debug.Log($"{actor} has no suitable weapon and skips his move");
                else
                    yield return MakeTurn(actor, target, weapon);
            }
            var winner = _leftTeam.IsAlive ? "It's a sound of police..." : "Bomb has been planted...";
            var winnerTeam = _leftTeam.IsAlive ? _leftTeam : _rightTeam;
            AudioManager.Instance.Play(winnerTeam.Controller.IsPC
                ? AudioSourceType.Win
                : AudioSourceType.Lose);
            DialogsController.Instance.Show(
                DialogType.WinLose,
                new WinLoseParameters
                {
                    Message = winner,
                    NextSceneName = _nextLevel,
                });
        }

        private IEnumerator MakeTurn(IActor member, IActor target, IWeapon weapon)
        {
            var mc = member as Character;
            var tc = target as Character;
            _selector.StartSelection(tc, false);
            if (weapon.MaxDistance < (mc.transform.position - tc.transform.position).magnitude && Mathf.Approximately(member.Speed, 0f))
            {
                Debug.LogWarning($"{member} can't move and skips attack");
                yield break;
            }

            mc.Attack.Equip(weapon);
            yield return mc.Moveable.MoveTo(tc.transform.position, weapon.MaxDistance);
            yield return mc.Attack.Attack(tc);
            _selector.StopSelection();
            yield return mc.Moveable.ReturnToBase();
        }

        internal void ResetGame()
        {
            StopAllCoroutines();
            _selector.StopSelection();
            foreach (var c in _leftTeam.Members.OfType<Character>())
                c.ResetGame();
            foreach (var c in _rightTeam.Members.OfType<Character>())
                c.ResetGame();
            StartCoroutine(Battle());
        }
    }
}
