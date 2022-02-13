using System;
using System.Collections.Generic;

namespace Assets.Scripts.Logic
{
    public class BattleLogic
    {
        private bool _isEndOfGame = false;
        private Dictionary<Guid, Team> _teams = new Dictionary<Guid, Team>();

        public Guid CreateTeam(IChooseTarget chooser)
        {
            var team = new Team(chooser);
            _teams.Add(team.Id, team);
            return team.Id; ;
        }

        public void AddTeamMember(Guid teamId, IActor actor)
        {
            if (_teams.TryGetValue(teamId, out var team))
                team.AddMember(actor);
        }

        internal bool IsEndOfGame => _isEndOfGame;

        public event Action<MoveEventArgs> MoveEvent;
        public event Action<MoveEventArgs> AttackEvent;
    }
}
