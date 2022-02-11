﻿using UnityEngine;

namespace Assets.Scripts
{
    internal class HealthComponent : MonoBehaviour
    {
        [SerializeField]
        private int _health;

        internal int Health
        {
            get => _health;
            set => _health = Mathf.Max(0, value);
        }

        internal bool IsDead => _health == 0;

        private AnimationControlComponent _animator;
        private void Awake()
        {
            _animator = GetComponentInChildren<AnimationControlComponent>();
        }

        internal void ReceiveDamage(int damage)
        {
            Health -= damage;
            if (IsDead)
            {
                _animator.Die();
            }
            else
                _animator.IsAngry = true;
        }
    }
}
