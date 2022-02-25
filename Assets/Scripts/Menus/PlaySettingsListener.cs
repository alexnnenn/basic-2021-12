using Assets.Scripts.Managers;
using Assets.Scripts.ScriptableObjectsProtos;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Menus
{
    public class PlaySettingsListener : MonoBehaviour
    {
        public GameSettings _settings;

        public void OnShownHidden()
        {
            if (DialogsController.Instance.IsShown(DialogType.PlaySettings))
            {
                Time.timeScale = 0f;
            }
        }

        public void OnRestartClicked()
        {
            Time.timeScale = 1f;
            TransitionManager.ReloadScene();
            DialogsController.Instance.Show(DialogType.None);
        }

        public void OnMenuClicked()
        {
            Time.timeScale = 1f;
            DialogsController.Instance.Show(DialogType.MainMenu);
            SceneManager.LoadScene("MainMenu");
        }

        public void OnCloseClicked()
        {
            Time.timeScale = 1f;
            DialogsController.Instance.Show(DialogType.None);
        }
    }
}