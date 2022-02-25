using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class MainMenuListener : MonoBehaviour
    {
        public void OnStartClicked()
        {
            DialogsController.Instance.Show(DialogType.StartOptions);
        }

        public void OnSettingsClicked()
        {
            DialogsController.Instance.Show(DialogType.StartSettings);
        }

        public void OnQuitClicked()
        {
            Application.Quit();
        }
    }
}