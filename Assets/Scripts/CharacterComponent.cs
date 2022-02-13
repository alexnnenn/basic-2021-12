using Assets.Scripts.Logic;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    internal class CharacterComponent : MonoBehaviour, IActor
    {
        private bool? _isAttacker;
        public bool IsAttacker => (_isAttacker ?? (_isAttacker = Attack != null)) == true;

        [SerializeField]
        private List<Weapon> _weapons;

        internal MoveableComponent Movable { get; private set; }
        internal HealthComponent Life { get; private set; }
        internal AttackerComponent Attack { get; private set; }
        internal IReadOnlyList<Weapon> Weapons => _weapons;
        internal HandMarker Hand { get; private set; }

        public bool IsDead => Life.IsDead;

        public int Health => Life.Health;

        private void Awake()
        {
            Movable = GetComponent<MoveableComponent>();
            Life = GetComponent<HealthComponent>();
            Attack = GetComponent<AttackerComponent>();
            Hand = GetComponentInChildren<HandMarker>();
        }
    }
}