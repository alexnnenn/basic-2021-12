using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    public class PlayerGroundController : MonoBehaviour
    {
        [SerializeField]
        private string _groundLayerName;

        [SerializeField]
        private UnityEvent _grounded;

        [SerializeField]
        private UnityEvent _ungrounded;

        private int _groundCollisions;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (LayerMask.LayerToName(collision.gameObject.layer) == _groundLayerName)
            {
                _groundCollisions++;
                UpdateGrounded();
            }
        }

        private void UpdateGrounded()
        {
            Debug.Log($"{this} {_groundCollisions}");
            if (_groundCollisions == 0)
            {
                _ungrounded.Invoke();
            }
            else
            {
                _grounded.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (LayerMask.LayerToName(collision.gameObject.layer) == _groundLayerName)
            {
                _groundCollisions--;
                UpdateGrounded();
            }
        }
    }
}
