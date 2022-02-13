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

        public GameObject Model;

        private bool? _modelExist;

        internal bool CreateObject(out GameObject newObject)
        {
            _modelExist ??= Model != null;
            if (_modelExist == true)
            {
                newObject = Instantiate(Model);
                return true;
            }
            newObject = null;
            return false;
        }
    }
}
