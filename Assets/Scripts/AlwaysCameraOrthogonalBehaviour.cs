using UnityEngine;

namespace Assets.Scripts
{
    internal sealed class AlwaysCameraOrthogonalBehaviour : MonoBehaviour
    {
        private Transform _camera;
        private void Awake()
        {
            _camera = Camera.main.transform;
        }

        private void Update()
        {
            transform.LookAt(_camera);
        }
    }
}
