using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon", order = 1)]
    public class Weapon : ScriptableObject
    {
        public bool UsesAmmo;

        public int Damage;

        public float MaxDistance;

        public WeaponType Type;
    }
}
