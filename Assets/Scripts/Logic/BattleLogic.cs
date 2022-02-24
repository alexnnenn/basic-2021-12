using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Logic
{
    public class BattleLogic
    {
        private List<Team> _teams = new List<Team>();

        public bool IsBattle => _teams.Count(t => t.Members.Any(m => m.HealthAmount > 0)) > 1;

        public void SetTeamOrder(Team team, int order)
        {
            if (!_teams.Contains(team))
                throw new ArgumentException("Команда не зарегистрирована");
            if (order < 0 || order >= _teams.Count)
                throw new ArgumentException("Неверный порядок");
            _teams.Remove(team);
            _teams.Insert(order, team);
        }

        public Team CreateTeam(ITargetChooser controller)
        {
            var team = new Team(controller);
            _teams.Add(team);
            return team;
        }

        public IEnumerable<IActor> EnumerateActors()
        {
            while (true)
                foreach (var team in _teams)
                {
                    foreach (var actor in team.Members.Where(a => a.HealthAmount > 0))
                    {
                        yield return actor;
                        if (!IsBattle)
                        {
                            yield break;
                        }
                    }
                }
        }

        internal void GetAction(IActor actor, Action<IActor, IWeapon> callback)
        {
            var ctr = actor.Team.Controller;
            var enemies = _teams
                .Where(t => t.Id != actor.Team.Id) //своих не бьем
                .SelectMany(t => t.Members)
                .ToList();
            ctr.Choose(actor, enemies, callback);
        }
    }
}
