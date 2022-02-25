using Assets.Scripts.Common;
using Assets.Scripts.ScriptableObjectsProtos;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class SetupMenuListener : MonoBehaviour
    {
        public UnityEngine.UI.Dropdown _leftCombo;
        public UnityEngine.UI.Dropdown _rightCombo;
        public GameSettings _settings;

        public void OnShownHidden()
        {
            if (DialogsController.Instance.IsShown(DialogType.StartSettings))
            {
                _leftCombo.value = (int)_settings.LeftControlledBy;
                _rightCombo.value = (int)_settings.RightControlledBy;
            }
        }

        public void OnLeftChanged()
        {
            _settings.LeftControlledBy = (ControlledBy)_leftCombo.value;
        }

        public void OnRightChanged()
        {
            _settings.RightControlledBy = (ControlledBy)_rightCombo.value;
        }

        public void OnBackClicked()
        {
            DialogsController.Instance.Show(DialogType.MainMenu);
        }
    }
}