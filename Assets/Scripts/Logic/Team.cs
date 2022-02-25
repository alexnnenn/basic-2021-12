using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Logic
{
    public class Team
    {
        public Guid Id { get; private set; }

        private List<IActor> _members = new List<IActor>();
        internal IReadOnlyList<IActor> Members => _members;
        internal ITargetChooser Controller { get; }

        public Team(ITargetChooser controller)
        {
            Controller = controller;
            Id = Guid.NewGuid();
        }

        public void AddMember(IActor member)
        {
            if (member.Team != null)
                throw new InvalidOperationException("Актер уже находится в другой команде");
            _members.Add(member);
            member.Team = this;
        }

        public bool IsAlive => _members.Any(m => m.HealthAmount > 0);
    }
}