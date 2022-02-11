using System;
using UnityEngine;

namespace Assets.Scripts
{
    internal class CharacterComponent : MonoBehaviour
    {
        internal MoveableComponent Movable { get; private set; }
        internal HealthComponent Health { get; private set; }
        internal AttackerComponent[] Attacks { get; private set; }

        void Awake()
        {
            Movable = GetComponent<MoveableComponent>();
            Health = GetComponent<HealthComponent>();
            Attacks = GetComponents<AttackerComponent>();
        }
    }
}