using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    internal class InventoryComponent : MonoBehaviour
    {
        public List<Ownage> Items;

        [Serializable]
        internal class Ownage
        {
            public int Amount;

            public ItemType Type;
        }
    }
}