using UnityEngine;

namespace Assets.Scripts
{
    public class DeathZoneController : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            CheckDeath(collision);
        }

        private void CheckDeath(Collider2D collision)
        {
            Debug.Log($"Object {collision.gameObject} entered {this.gameObject}");
            var alive = collision.gameObject.GetComponentInParent<LiveBeingController>();
            if (alive != null && alive.CanDieInZone && alive.IsAlive)
            {
                Debug.Log($"{this.gameObject} affects {collision.gameObject}");
                alive.DieImmediately();
            }
        }
    }
}
