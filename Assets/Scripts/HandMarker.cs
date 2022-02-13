using UnityEngine;

namespace Assets.Scripts
{
    internal class HandMarker : MonoBehaviour
    {
        private static readonly Quaternion ZeroRotation = Quaternion.Euler(0f, 0f, 0f);

        internal void SetWeapon(Weapon weapon)
        {
            if (transform.childCount != 0)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }

            if (weapon.CreateObject(out var newObject))
            {
                newObject.transform.SetParent(transform, false);
                newObject.transform.localPosition = Vector3.zero;
                newObject.transform.localRotation = ZeroRotation;
            }
        }
    }
}
