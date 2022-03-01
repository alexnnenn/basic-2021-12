using Assets.Scripts.Logic;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    internal class Character : MonoBehaviour, IActor
    {
        internal MoveableComponent Moveable { get; private set; }
        internal HealthComponent Health { get; private set; }
        internal AttackerComponent Attack { get; private set; }
        internal InventoryComponent Inventory { get; private set; }
        internal AnimationControlComponent Animator { get; private set; }

        public int HealthAmount => Health?.Health ?? -1;

        public float Speed => Moveable?.Speed ?? 0f;

        public Team Team { get; set; }

        public IReadOnlyDictionary<IItem, int> Items => Inventory.Items.GroupBy(i => i.Type).ToDictionary(g => (IItem)g.Key, g => g.Select(o => o.Amount).Sum());

        public void RemoveItem(IItem item)
        {
            var ownage = Inventory.Items.FirstOrDefault(i => i.Type.Id == item.Id);
            ownage.Amount--;
        }

        private void Awake()
        {
            Moveable = GetComponent<MoveableComponent>();
            Health = GetComponent<HealthComponent>();
            Attack = GetComponent<AttackerComponent>();
            Inventory = GetComponent<InventoryComponent>();
            Animator = GetComponentInChildren<AnimationControlComponent>();
        }

        internal void ResetGame()
        {
            Health.ResetGame();
            Inventory.ResetGame();
        }
    }
}