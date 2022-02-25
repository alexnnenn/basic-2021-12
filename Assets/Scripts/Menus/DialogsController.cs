using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class DialogsController : MonoBehaviour
    {
        internal static DialogsController Instance { get; private set; }

        [SerializeField]
        private Dialog[] _dialogs;

        private DialogType _shown;

        public void Show(DialogType dialogType)
        {
            _shown = dialogType;
            foreach (var dialog in _dialogs)
            {
                dialog.SetShown<object>(dialog.Type == dialogType, null);
            }
        }

        public void Show<T>(DialogType dialogType, T parameter)
        {
            _shown = dialogType;
            foreach (var dialog in _dialogs)
            {
                dialog.SetShown(dialog.Type == dialogType, parameter);
            }
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            Show(DialogType.MainMenu);
        }

        internal bool IsShown(DialogType dialogType) => _shown == dialogType;
    }
}