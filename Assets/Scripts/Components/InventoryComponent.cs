using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    internal class InventoryComponent : MonoBehaviour
    {
        public List<Ownage> _defaultItems;

        public List<Ownage> Items { get; private set; }

        private void Awake()
        {
            ResetGame();
        }

        internal void ResetGame()
        {
            Items = new List<Ownage>();
            foreach(var own in _defaultItems)
            {
                Items.Add(new Ownage { 
                    Amount = own.Amount,
                    Type = own.Type,
                });
            }
        }

        [Serializable]
        public class Ownage
        {
            public int Amount;

            public ItemType Type;
        }
    }
}