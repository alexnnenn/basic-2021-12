using Assets.Scripts.Logic;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class PanelUseWeaponButton : PanelButton
    {
        private AmmoTextComponent _ammoText;
        private IReadOnlyDictionary<IItem, int> _items;

        [SerializeField]
        private WeaponType _weapon;
        internal WeaponType Weapon => _weapon;

        internal bool IsUsable => _weapon.AmmoType == null || GetAmmoAmount() > 0;

        internal void SetItems(IReadOnlyDictionary<IItem, int> items)
        {
            _items = items;
            var ammo = _weapon.AmmoType;
            if(ammo != null)
            {
                _ammoText.Ammo = GetAmmoAmount();
            }
            else
            {
                _ammoText.Ammo = null;
            }
        }

        private int GetAmmoAmount()
        {
            var ammo = _weapon.AmmoType;
            return _items.TryGetValue(ammo, out var amount) ? amount : 0;
        }

        protected override bool IsVisibleInternal() => _weapon != null && _items.TryGetValue(_weapon, out var amount) && amount > 0;

        private void Awake()
        {
            _ammoText = GetComponentInChildren<AmmoTextComponent>();
            _ammoText.Ammo = null;
        }
    }
}
