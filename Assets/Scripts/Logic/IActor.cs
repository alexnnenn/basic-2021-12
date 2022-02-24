using System.Collections.Generic;

namespace Assets.Scripts.Logic
{
    public interface IActor
    {
        int HealthAmount { get; }

        float Speed { get; }

        Team Team { get; set; }

        IReadOnlyDictionary<IItem, int> Items { get; }

        void RemoveItem(IItem item);
    }
}
