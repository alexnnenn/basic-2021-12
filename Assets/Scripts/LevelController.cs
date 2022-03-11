using Assets.Scripts.Dialogs;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField]
        private PlayerController _player;

        [SerializeField]
        private Transform _playerSpawnPoint;

        private void Awake()
        {
            Restart();
        }

        internal void Restart()
        {
            Debug.Log("Restarting level");
            _player.transform.position = _playerSpawnPoint.position;
            _player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            _player.GetComponent<LiveBeingController>().IsAlive = true;
            _player.SetFootUngrounded();
            _player.SetLeftUngrounded();
            _player.SetRightUngrounded();
        }

        public void PlayerDeath()
        {
            Debug.Log("Killing player");
            DialogManager.Instance.Show(DialogType.GameOver);
        }
    }
}
