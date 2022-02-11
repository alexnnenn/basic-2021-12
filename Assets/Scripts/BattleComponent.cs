using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    internal class BattleComponent : MonoBehaviour
    {
        [SerializeReference]
        private CharacterComponent[] LeftTeam;

        [SerializeReference]
        private CharacterComponent[] RightTeam;

        private UIComponent _ui;

        private void Awake()
        {
            _ui = GetComponentInChildren<UIComponent>();
        }

        private void Start()
        {
            StartCoroutine(Battle());
        }

        private IEnumerator Battle()
        {
            var roundNumber = 1;
            var isRightTeamMove = true;
            while (BothCommandsAlive())
            {
                if (isRightTeamMove)
                    yield return _ui.ShowRound(roundNumber);

                var activeTeam = isRightTeamMove ? RightTeam : LeftTeam;
                var targetsTeam = isRightTeamMove ? LeftTeam : RightTeam;

                var targetsAlive = true;
                foreach (var activeTeamMember in activeTeam.Where(c => !c.Health.IsDead))
                {
                    Debug.Log($"{activeTeamMember} is making his turn");
                    yield return MakeTurn(activeTeamMember, targetsTeam);
                    Debug.Log($"{activeTeamMember} ended his turn");
                    targetsAlive = TeamAlive(targetsTeam);
                    if (!targetsAlive)
                        break;
                }

                if (!targetsAlive)
                    break;

                isRightTeamMove = !isRightTeamMove;
                if (isRightTeamMove)
                    roundNumber++;
            }
        }

        private IEnumerator MakeTurn(CharacterComponent teamMember, CharacterComponent[] enemies)
        {
            var moveable = teamMember.Movable;
            if (moveable == null || Mathf.Approximately(0f, moveable.Speed))
            {
                Debug.LogWarning($"{teamMember} cant move");
                yield break;
            }

            var bestAttack = teamMember.Attacks.Where(a => a.CanBeUsed).OrderByDescending(a => a.AttackDamage).FirstOrDefault();
            if (bestAttack == null)
            {
                Debug.LogWarning($"{teamMember} has no usable attacks");
                yield break;
            }

            var target = enemies
                .Where(e => !e.Health.IsDead)
                .OrderBy(e => Mathf.Abs(e.Health.Health - bestAttack.AttackDamage))
                .FirstOrDefault();

            bestAttack.Use();
            yield return moveable.MoveTo(target.transform.position, bestAttack.AttackDistance);
            yield return bestAttack.Attack(target);
            yield return moveable.ReturnToBase();
        }

        private bool BothCommandsAlive() => TeamAlive(LeftTeam) && TeamAlive(RightTeam);

        private bool TeamAlive(CharacterComponent[] team) => team.Any(c => !c.Health.IsDead);
    }
}
