using Assets.Scripts.Logic;
using Assets.Scripts.Managers;
using Assets.Scripts.Menus;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    internal class AttackerComponent : MonoBehaviour
    {
        private Character _self;
        private IWeapon _equippedWeapon = null;

        private AnimationControlComponent _animator;
        private void Awake()
        {
            _self = GetComponentInParent<Character>();
            _animator = GetComponentInChildren<AnimationControlComponent>();
            _animator.AttackAnimationEnded += OnAttackAnimationEnded;
        }

        private bool _attackEnded;
        private void OnAttackAnimationEnded() => _attackEnded = true;

        internal IEnumerator Attack(Character target)
        {
            _attackEnded = false;
            switch (_equippedWeapon)
            {
                case { IsProjectile: true } when _equippedWeapon.AmmoType == null || _self.Items.TryGetValue(_equippedWeapon.AmmoType, out var amount) && amount > 0:
                    _animator.Shoot();
                    _self.RemoveItem(_equippedWeapon.AmmoType);
                    break;
                case { IsProjectile: false, IsMartial: false }:
                    _animator.BatStrike();
                    break;
                default:
                    _animator.HandStrike();
                    break;
            }
            yield return new WaitUntil(() => _attackEnded);
            target.Health.ReceiveDamage(_equippedWeapon.Damage);
            yield return new WaitForFixedUpdate();
        }

        internal void Equip(IWeapon weapon)
        {
            _equippedWeapon = weapon;
            var w = weapon as WeaponType;
            _animator.SetWeapon(w.Prototype, w?.LocalRotation, w?.LocalPosition);
        }
    }
}
