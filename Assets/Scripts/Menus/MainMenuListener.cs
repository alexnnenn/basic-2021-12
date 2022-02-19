using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class MainMenuListener : MonoBehaviour
    {
        public DialogsComponent _dialogsComponent;

        public void OnStartClicked()
        {
            _dialogsComponent.Show(DialogType.StartOptions);
        }

        public void OnSettingsClicked()
        {
            _dialogsComponent.Show(DialogType.StartSettings);
        }

        public void OnQuitClicked()
        {
            Application.Quit();
        }
    }
}