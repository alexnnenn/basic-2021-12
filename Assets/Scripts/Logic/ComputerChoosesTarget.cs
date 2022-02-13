using System;
using System.Linq;

namespace Assets.Scripts.Logic
{
    public sealed class ComputerChoosesTarget : IChooseTarget
    {
        private IActor[] _enemies;

        public ComputerChoosesTarget(IActor[] enemies)
        {
            _enemies = enemies;
        }

        public void Choose(Action<IActor> callback)
        {
            var target = _enemies
                .Where(e => !e.IsDead)
                .OrderBy(e => e.Health)
                .FirstOrDefault();
            callback(target);
        }
    }
}
