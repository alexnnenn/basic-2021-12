using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    internal class MoveableComponent : MonoBehaviour
    {
        [SerializeField]
        private float _runSpeed;

        public float Speed => _runSpeed;

        private Vector3 originalPosition;
        private Quaternion originalRotation;
        private AnimationControlComponent _animator;

        private void Awake()
        {
            _animator = GetComponentInChildren<AnimationControlComponent>();
            originalPosition = transform.position;
            originalRotation = transform.rotation;
        }

        public IEnumerator MoveTo(Vector3 targetPosition, float distanceToStop)
        {
            Vector3 startDistance = targetPosition - transform.position;
            if (startDistance.magnitude < distanceToStop)
                yield break;
            do
            {
                Vector3 distance = targetPosition - transform.position;
                if (distance.magnitude < 0.00001f)
                {
                    transform.position = targetPosition;
                    _animator.Stop();
                    yield break;
                }
                else
                {
                    _animator.Run();
                }

                Vector3 direction = distance.normalized;
                transform.rotation = Quaternion.LookRotation(direction);

                var stopPosition = targetPosition - direction * distanceToStop;
                distance = (stopPosition - transform.position);

                Vector3 step = direction * _runSpeed;
                if (step.magnitude < distance.magnitude)
                {
                    transform.position += step;
                    yield return new WaitForFixedUpdate();
                    continue;
                }
                else
                {
                    transform.position = stopPosition;
                    _animator.Stop();
                    yield break;
                }
            }
            while (true);
        }

        internal IEnumerator ReturnToBase()
        {
            yield return MoveTo(originalPosition, 0f);
            transform.rotation = originalRotation;
        }
    }
}
