using System;
using UnityEngine;

namespace Assets.Scripts
{
    internal class AnimationControlComponent : MonoBehaviour
    {
        private static readonly int IsDeadKey = Animator.StringToHash("IsDead");
        private static readonly int IsAngryKey = Animator.StringToHash("IsAngry");
        private static readonly int RunningKey = Animator.StringToHash("Running");
        private static readonly int ShootKey = Animator.StringToHash("Shoot");
        private static readonly int DeathKey = Animator.StringToHash("Death");
        private static readonly int HandStrikeKey = Animator.StringToHash("HandStrike");
        private static readonly int BatStrikeKey = Animator.StringToHash("BatStrike");

        public bool IsAngry;
        public bool IsDead;

        private Animator _animator;

        public bool HasPistol { get; internal set; }

        void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            _animator.SetBool(IsDeadKey, IsDead);
            _animator.SetBool(IsAngryKey, IsAngry);   
        }

        public event Action AttackAnimationEnded;
        public event Action DeathAnimationEnded;

        internal void Stop() => _animator.SetBool(RunningKey, false);

        internal void Run() => _animator.SetBool(RunningKey, true);

        internal void Shoot() => _animator.SetTrigger(ShootKey);

        internal void Die() => _animator.SetTrigger(DeathKey);

        internal void HandStrike() => _animator.SetTrigger(HandStrikeKey);

        internal void MeleeStrike() => _animator.SetTrigger(BatStrikeKey);

        private void Shooted() => AttackAnimationEnded?.Invoke();

        private void BatStriked() => AttackAnimationEnded?.Invoke();

        private void HandStriked() => AttackAnimationEnded?.Invoke();

        private void Dying() => DeathAnimationEnded?.Invoke();
    }
}
