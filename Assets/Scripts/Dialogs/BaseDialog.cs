using UnityEngine;

namespace Assets.Scripts.Dialogs
{
    public class BaseDialog : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _selfGroup;

        internal void Close()
        {
            Debug.Log($"Closing {gameObject}");
            Time.timeScale = 1f;
            _selfGroup.alpha = 0f;
            _selfGroup.interactable = false;
            _selfGroup.blocksRaycasts = false;
        }

        internal void Show()
        {
            Debug.Log($"Showing {gameObject}");
            _selfGroup.alpha = 1f;
            _selfGroup.interactable = true;
            _selfGroup.blocksRaycasts = true;
            Time.timeScale = 0f;
        }
    }
}
