using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    internal class AttackerComponent : MonoBehaviour
    {
        private Weapon _lastWeapon;
        private CharacterComponent _char;
        private AnimationControlComponent _animator;

        [SerializeField]
        private int _ammo;
        internal int Ammo
        {
            get => _ammo;
            set => _ammo = Mathf.Max(0, value);
        }

        private void Awake()
        {
            _char = GetComponent<CharacterComponent>();
            _animator = GetComponentInChildren<AnimationControlComponent>();
            _animator.AttackAnimationEnded += OnAttackAnimationEnded;
        }

        private bool _attackEnded;
        private void OnAttackAnimationEnded() => _attackEnded = true;

        internal IEnumerator Attack(CharacterComponent target)
        {
            _attackEnded = false;
            switch (_lastWeapon)
            {
                case { Type : WeaponType.Melee }:
                    _animator.MeleeStrike();
                    break;
                case { Type: WeaponType.Projectile } when _ammo > 0 || !_lastWeapon.UsesAmmo:
                    _animator.Shoot();
                    if(_lastWeapon.UsesAmmo)
                        _ammo--;
                    break;
                default:
                    _animator.HandStrike();
                    break;
            }
            yield return new WaitUntil(() => _attackEnded);
            target.Life.ReceiveDamage(_lastWeapon.Damage);
            yield return new WaitForFixedUpdate();
        }

        internal void Use(Weapon weapon)
        {
            if(weapon != _lastWeapon)
            {
                _lastWeapon = weapon;
                _char.Hand.SetWeapon(weapon);
            }
        }

        internal bool CanUse(Weapon weapon) => !weapon.UsesAmmo || _ammo > 0;
    }
}
