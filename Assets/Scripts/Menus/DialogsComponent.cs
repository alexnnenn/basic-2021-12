using System;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class DialogsComponent : MonoBehaviour
    {
        [SerializeField]
        private Dialog[] _dialogs;

        private DialogType _shown;

        public void Show(DialogType dialogType)
        {
            _shown = dialogType;
            foreach(var dialog in _dialogs)
            {
                dialog.SetShown(dialog.Type == dialogType);
            }
        }

        private void Start()
        {
            Show(DialogType.MainMenu);
        }

        internal bool IsShown(DialogType dialogType) => _shown == dialogType;
    }
}