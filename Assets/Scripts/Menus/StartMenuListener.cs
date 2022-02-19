using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Menus
{
    public class StartMenuListener : MonoBehaviour
    {
        public DialogsComponent _dialogsComponent;

        public void OnLevel1Clicked()
        {
            SceneManager.LoadScene("Level01");
        }

        public void OnLevel2Clicked()
        {
            SceneManager.LoadScene("Level02");
        }

        public void OnBackClicked()
        {
            _dialogsComponent.Show(DialogType.MainMenu);
        }
    }
}