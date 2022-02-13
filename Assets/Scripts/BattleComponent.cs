using Assets.Scripts.Logic;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    internal class BattleComponent : MonoBehaviour
    {
        [SerializeReference]
        private IActor[] PlayerTeam;

        [SerializeReference]
        private IActor[] ComputerTeam;

        private PlayerMoveComponent _playerMove;
        private UIComponent _ui;
        private BattleLogic _logic;

        private void Awake()
        {
            _ui = GetComponentInChildren<UIComponent>();
            _playerMove = GetComponent<PlayerMoveComponent>();

            _logic = new BattleLogic();
            var playerTeamId = _logic.CreateTeam(_playerMove);
            foreach(var character in PlayerTeam)
                _logic.AddTeamMember(playerTeamId, character);

            var pcTeamId = _logic.CreateTeam(new ComputerChoosesTarget(PlayerTeam));
            foreach (var character in ComputerTeam)
                _logic.AddTeamMember(pcTeamId, character);

            _logic.MoveEvent += OnMoveEvent;
        }

        private void OnDestroy()
        {
            
        }

        private void Start()
        {
            StartCoroutine(Battle());
        }

        private IEnumerator Battle()
        {
            var roundNumber = 1;
            while (!_logic.IsEndOfGame)
            {
                yield return _ui.ShowRound(roundNumber++);

                foreach (var enemy in ComputerTeam.Where(c => !c.IsDead))
                {
                    using (new CharacterActivator(enemy))
                        yield return MakeTurn(enemy);
                }

                foreach (var player in PlayerTeam.Where(c => !c.IsDead))
                {
                    using (new CharacterActivator(enemy))
                        yield return MakeTurn(player);
                }
            }
        }

        private bool ChooseWeapon(IActor character, out WeaponInfo weapon)
        {
            weapon = null;
            if (!character.IsAttacker)
                return false;

            var bestWeapon = character.Weapons
                .Where(character.Attack.CanUse)
                .OrderByDescending(weapon => weapon.Damage)
                .FirstOrDefault();
            if (bestWeapon == null)
            {
                Debug.LogWarning($"{character} has no usable attacks");
                return false;
            }
            weapon = bestWeapon;
            return true;
        }

        private IEnumerator MakeTurn(IActor character)
        {
            if (!ChooseWeapon(character, out var weapon))
                yield break;

            var target = enemies
                .Where(e => !e.Life.IsDead)
                .OrderBy(e => Mathf.Abs(e.Life.Health - weapon.Damage))
                .FirstOrDefault();

            character.Attack.Use(weapon);
            yield return moveable.MoveTo(target.transform.position, weapon.MaxDistance);
            yield return character.Attack.Attack(target);
            yield return moveable.ReturnToBase();
        }

        private bool BothCommandsAlive() => TeamAlive(PlayerTeam) && TeamAlive(ComputerTeam);

        private bool TeamAlive(CharacterComponent[] team) => team.Any(c => !c.Life.IsDead);
    }
}
