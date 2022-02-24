using Assets.Scripts.Common;
using Assets.Scripts.Components;
using Assets.Scripts.Logic;
using Assets.Scripts.ScriptableObjectsProtos;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    internal class BattleOrchestrator : MonoBehaviour
    {
        [SerializeReference]
        private Character[] LeftTeam;
        [SerializeReference]
        private Character[] RightTeam;
        [SerializeReference]
        private GameSettings Settings;
        private CharacterSelectionSystem _selector;
        private UIComponent _ui;
        private SkillsPanelComponent _skills;

        private BattleLogic _logic;
        private Team _leftTeam;
        private Team _rightTeam;
        private Guid _firstTeamId;
        private PlayerTargetChooser _playerCtr;

        private void Awake()
        {
            _selector = GetComponent<CharacterSelectionSystem>();
            _playerCtr = GetComponent<PlayerTargetChooser>();
            Debug.Log($"Ctr in orch hash={_playerCtr.GetHashCode()}");
            _ui = GetComponentInChildren<UIComponent>();
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
            StartCoroutine(Battle());
        }

        private IEnumerator Battle()
        {
            var roundNumber = 1;
            var previousTeamId = Guid.Empty;
            foreach(var actor in _logic.EnumerateActors())
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
    }
}
