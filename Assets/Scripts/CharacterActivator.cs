using Assets.Scripts.Logic;
using System;

namespace Assets.Scripts
{
    internal class CharacterActivator : IDisposable
    {
        private IActor enemy;

        public CharacterActivator(IActor enemy)
        {
            this.enemy = enemy;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}