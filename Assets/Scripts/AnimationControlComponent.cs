using System;
using UnityEngine;

namespace Assets.Scripts
{
    internal class AnimationControlComponent : MonoBehaviour
    {
        public bool IsAngry;
        public bool IsDead;
        public bool HasPistol;

        private Animator _animator;
        private PistolMarker _pistol;

        void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _pistol = GetComponentInChildren<PistolMarker>();
        }

        private void Update()
        {
            _animator.SetBool("IsDead", IsDead);
            if (_pistol != null)
            {
                _pistol.gameObject.SetActive(HasPistol);
            }
            _animator.SetBool("IsAngry", IsAngry);   
        }

        public event Action AttackAnimationEnded;
        public event Action DeathAnimationEnded;

        internal void Stop() => _animator.SetBool("Running", false);

        internal void Run() => _animator.SetBool("Running", true);

        internal void Shoot() => _animator.SetTrigger("Shoot");

        internal void Die() => _animator.SetTrigger("Death");

        internal void HandStrike() => _animator.SetTrigger("HandStrike");

        internal void BatStrike() => _animator.SetTrigger("BatStrike");

        private void Shooted()
        {
            AttackAnimationEnded?.Invoke();
            /*character.Ammo--;
            character.target.Damage(2);
            character.SetState(CharacterComponent.State.Idle);*/
        }

        private void BatStriked()
        {
            AttackAnimationEnded?.Invoke();
            //AttackAnimationEnded?.Invoke();
            /*character.target.Damage(1);
            character.SetState(CharacterComponent.State.RunningFromEnemy);*/
        }

        private void HandStriked()
        {
            AttackAnimationEnded?.Invoke();
        }

        private void Dying()
        {
            DeathAnimationEnded?.Invoke();
            //character.SetState(CharacterComponent.State.Dead);
        }
    }
}
