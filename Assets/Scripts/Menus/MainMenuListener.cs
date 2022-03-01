using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class MainMenuListener : MonoBehaviour
    {
        public void OnStartClicked()
        {
            AudioManager.Instance.Play(AudioSourceType.Click);
            DialogsController.Instance.Show(DialogType.StartOptions);
        }

        public void OnSettingsClicked()
        {
            AudioManager.Instance.Play(AudioSourceType.Click);
            DialogsController.Instance.Show(DialogType.StartSettings);
        }

        public void OnQuitClicked()
        {
            AudioManager.Instance.Play(AudioSourceType.Click);
            Application.Quit();
        }
    }
}