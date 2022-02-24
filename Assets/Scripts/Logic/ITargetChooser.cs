using System;
using System.Collections.Generic;

namespace Assets.Scripts.Logic
{
    public interface ITargetChooser
    {
        bool IsPC { get; }

        void Choose(IActor actor, List<IActor> enemies, Action<IActor, IWeapon> callback);
    }
}
