using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class LiveBeingController : MonoBehaviour
    {
        [SerializeField]
        private bool _canDieInZone;

        [SerializeField]
        private UnityEvent _momentoMori;

        internal bool CanDieInZone => _canDieInZone;

        internal void DieImmediately()
        {
            IsAlive = false;
            _momentoMori.Invoke();
        }

        internal bool IsAlive { get; set; }
    }
}
