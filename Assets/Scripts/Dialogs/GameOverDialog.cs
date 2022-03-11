using UnityEngine;

namespace Assets.Scripts.Dialogs
{
    public class GameOverDialog : BaseDialog
    {
        [SerializeField]
        private LevelController _level;

        public void OnRestartClicked()
        {
            _level.Restart();
            Close();
        }
    }
}
