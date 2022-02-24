using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Logic
{
    public class ComputerTargetChooser : ITargetChooser
    {
        private static readonly HealthWeaponComparer _hwComparer = new HealthWeaponComparer();

        public bool IsPC => false;

        public void Choose(IActor actor, List<IActor> enemies, Action<IActor, IWeapon> callback)
        {
            var bestWeapon = actor.Items.Keys.OfType<IWeapon>() //Выбираем оружие
                .Where(w => CanBeUsed(w, actor)) // к которому есть патроны или рукопашное
                .OrderByDescending(a => a.Damage) // самое лучшее
                .FirstOrDefault();

            if (bestWeapon == null)
            {
                callback(null, null);
                return;
            }

            var target = enemies
                .Where(a => a.HealthAmount > 0) // трупы не пинаем
                .OrderBy(a => Math.Abs(a.HealthAmount - bestWeapon.Damage), _hwComparer)
                .FirstOrDefault();

            callback(target, bestWeapon);
        }

        private bool CanBeUsed(IWeapon weapon, IActor actor)
        {
            return !weapon.IsProjectile || actor.Items.TryGetValue(weapon.AmmoType, out var ammo) && ammo > 0;
        }

        private class HealthWeaponComparer : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                var d = x - y;
                if (d == 0)
                    return 0;
                if (x == 0 || x < 0 && d > 0 || x < 0 && x > d || x > 0 && x < d)
                    return -1;
                return 1;
            }
        }
    }
}
