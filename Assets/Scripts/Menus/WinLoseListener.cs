using Assets.Scripts.Managers;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    internal class WinLoseListener : BaseListener
    {
        private string _next;
        public Text _text;

        public void OnShownHidden()
        {
            if (DialogsController.Instance.IsShown(DialogType.WinLose))
            {
                if (_parameter is WinLoseParameters w)
                {
                    _next = w.NextSceneName;
                    _text.text = w.Message;
                }
            }
        }

        public void OnRestartClicked()
        {
            DialogsController.Instance.Show(DialogType.None);
            TransitionManager.ReloadScene();
        }

        public void OnMenuClicked()
        {
            DialogsController.Instance.Show(DialogType.MainMenu);
            SceneManager.LoadScene("MainMenu");
        }

        public void OnNextClicked()
        {
            DialogsController.Instance.Show(DialogType.None);
            TransitionManager.GoToScene(_next);
        }
    }
}