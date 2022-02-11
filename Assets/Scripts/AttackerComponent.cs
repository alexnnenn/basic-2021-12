using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    internal class AttackerComponent : MonoBehaviour
    {
        [SerializeReference]
        private Weapon _attackWeapon;

        [SerializeField]
        private int _ammo;

        public bool CanBeUsed => _attackWeapon != null && (!_attackWeapon.UsesAmmo || Ammo > 0);

        internal float AttackDistance => _attackWeapon.MaxDistance;

        internal int AttackDamage => _attackWeapon.Damage;

        internal int Ammo
        {
            get => _ammo;
            set => _ammo = Mathf.Max(0, value);
        }

        private AnimationControlComponent _animator;
        private void Awake()
        {
            _animator = GetComponentInChildren<AnimationControlComponent>();
            _animator.HasPistol = _attackWeapon.UsesAmmo && Ammo > 0;
            _animator.AttackAnimationEnded += OnAttackAnimationEnded;
        }

        private bool _attackEnded;
        private void OnAttackAnimationEnded() => _attackEnded = true;

        internal IEnumerator Attack(CharacterComponent target)
        {
            _attackEnded = false;
            switch (_attackWeapon.Type)
            {
                case WeaponType.Bat:
                    _animator.BatStrike();
                    break;
                case WeaponType.Pistol when _ammo > 0:
                    _animator.Shoot();
                    _ammo--;
                    break;
                default:
                    _animator.HandStrike();
                    break;
            }
            yield return new WaitUntil(() => _attackEnded);
            _animator.HasPistol = _ammo > 0;
            target.Health.ReceiveDamage(AttackDamage);
            yield return new WaitForFixedUpdate();
        }

        public override string ToString() => $"Attack with {_attackWeapon.Type} for {AttackDamage} pts";

        internal void Use() => _animator.HasPistol = _ammo > 0;
    }
}
