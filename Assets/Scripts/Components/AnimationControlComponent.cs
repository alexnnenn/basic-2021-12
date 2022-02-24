using System;
using UnityEngine;

namespace Assets.Scripts
{
    internal class AnimationControlComponent : MonoBehaviour
    {
        private static readonly Quaternion ZeroRotation = Quaternion.Euler(0f, 0f, 0f);

        [SerializeField]
        private GameObject _weaponContainer;

        private bool _isAngry;
        public bool IsAngry
        {
            get => _isAngry;
            set
            {
                _isAngry = value;
                _animator.SetBool("IsAngry", IsAngry);
            }
        }

        private bool _isDead;
        public bool IsDead
        {
            get => _isDead;
            set
            {
                _isDead = value;
                _animator.SetBool("IsDead", IsDead);
            }
        }

        private Animator _animator;

        void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public event Action AttackAnimationEnded;
        public event Action DeathAnimationEnded;

        internal void Stop() => _animator.SetBool("Running", false);

        internal void Run() => _animator.SetBool("Running", true);

        internal void Shoot() => _animator.SetTrigger("Shoot");

        internal void Die() => _animator.SetTrigger("Death");

        internal void HandStrike() => _animator.SetTrigger("HandStrike");

        internal void BatStrike() => _animator.SetTrigger("BatStrike");

        private void Shooted() => AttackAnimationEnded?.Invoke();

        private void BatStriked() => AttackAnimationEnded?.Invoke();

        private void HandStriked() => AttackAnimationEnded?.Invoke();

        private void Dying() => DeathAnimationEnded?.Invoke();

        internal void SetWeapon(GameObject prefab, Quaternion? rotation, Vector3? position)
        {
            if (_weaponContainer.transform.childCount > 0)
            {
                // Убраем предыдущее оружие, если было
                Destroy(_weaponContainer.transform.GetChild(0).gameObject);
            }
            if (prefab != null)
            {
                var weaponInstance = Instantiate(prefab);
                weaponInstance.transform.SetParent(_weaponContainer.transform, false);
                weaponInstance.transform.localPosition = position ?? Vector3.zero;
                weaponInstance.transform.localRotation = rotation ?? ZeroRotation;
            }
        }
    }
}
