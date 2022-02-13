using System;

namespace Assets.Scripts.Logic
{
    public interface IChooseTarget
    {
        void Choose(Action<IActor> callback);
    }
}
