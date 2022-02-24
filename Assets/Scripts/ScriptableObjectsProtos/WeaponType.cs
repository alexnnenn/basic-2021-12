using Assets.Scripts.Logic;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "WeaponType", menuName = "ScriptableObjects/Weapon", order = 1)]
    public class WeaponType : ItemType, IWeapon
    {
        [SerializeField]
        private bool _martial;

        [SerializeField]
        private bool _projectile;

        [SerializeField]
        private int _damage;

        [SerializeReference]
        private ItemType _ammoType;

        [SerializeField]
        private float _distance;

        public bool IsMartial => _martial;

        public bool IsProjectile => _projectile;

        public IItem AmmoType => _ammoType;

        public int Damage => _damage;

        public float MaxDistance => _distance;
    }
}
