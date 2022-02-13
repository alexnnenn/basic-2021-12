using System;
using System.Collections.Generic;

namespace Assets.Scripts.Logic
{
    public sealed class Team
    {
        private readonly IChooseTarget _chooser;
        private List<IActor> _members = new List<IActor>();

        public Guid Id { get; } = Guid.NewGuid();

        public Team(IChooseTarget chooser)
        {
            this._chooser = chooser;
        }

        public void AddMember(IActor member)
        {
            _members.Add(member);
        }
    }
}
