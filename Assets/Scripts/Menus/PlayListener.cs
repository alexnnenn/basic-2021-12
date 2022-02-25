using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class PlayListener : MonoBehaviour
    {
        public void OnOpenClicked()
        {
            if (!DialogsController.Instance.IsShown(DialogType.PlaySettings))
            {
                Time.timeScale = 0f;
                DialogsController.Instance.Show(DialogType.PlaySettings);
            }
        }
    }
}