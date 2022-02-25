using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class StartMenuListener : MonoBehaviour
    {
        public void OnLevel1Clicked()
        {
            DialogsController.Instance.Show(DialogType.None);
            TransitionManager.GoToScene("Level01");
        }

        public void OnLevel2Clicked()
        {
            DialogsController.Instance.Show(DialogType.None);
            TransitionManager.GoToScene("Level02");
        }

        public void OnBackClicked()
        {
            DialogsController.Instance.Show(DialogType.MainMenu);
        }
    }
}