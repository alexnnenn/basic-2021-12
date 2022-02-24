using Assets.Scripts.Logic;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "ItemType", menuName = "ScriptableObjects/Item", order = 1)]
    public class ItemType : ScriptableObject, IItem
    {
        [SerializeField]
        private int _id;

        public GameObject Prototype;

        [SerializeField]
        private Vector3 _localRotationInHand;
        public Quaternion LocalRotation => Quaternion.Euler(_localRotationInHand);

        [SerializeField]
        private Vector3 _localPositionInHand;
        public Vector3 LocalPosition => _localPositionInHand;

        public int Id => _id;
    }
}
