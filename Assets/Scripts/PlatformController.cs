using UnityEngine;

namespace Assets.Scripts
{
    public class PlatformController : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _points;

        [SerializeField]
        private int _targetIndex;

        [SerializeField]
        private float _speed;

        private void FixedUpdate()
        {
            if (_points.Length == 0)
                return;

            if (_targetIndex < 0)
                _targetIndex = _points.Length + _targetIndex % _points.Length;

            if (_targetIndex >= _points.Length)
                _targetIndex = _targetIndex % _points.Length;

            var distance = (Vector2)(_points[_targetIndex].position - transform.position);
            
            var frameSpeed = _speed * Time.fixedDeltaTime;
            if (distance.magnitude < frameSpeed)
            {
                transform.position = _points[_targetIndex++].position;
            }
            else
            {
                transform.position += frameSpeed * (Vector3)distance.normalized;
            }
        }
    }
}
