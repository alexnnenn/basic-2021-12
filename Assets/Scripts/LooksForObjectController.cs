using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts
{
    public class LooksForObjectController : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;

        [SerializeField]
        private float _maxDistance;

        private void Update()
        {
            var currentPosition = transform.position;
            var way = (Vector2)(_target.position - currentPosition);
            var distanceDiff = way.magnitude - _maxDistance;
            if (distanceDiff > 0)
            {
                var newPosition = transform.position + (Vector3)way.normalized * distanceDiff;
                transform.position = newPosition.Quantize();
            }
        }
    }
}
