using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Dialogs
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField]
        private BaseDialog _gameOverDialog;

        internal static DialogManager Instance { get; private set; }

        private Dictionary<DialogType, BaseDialog> _dialogs = new Dictionary<DialogType, BaseDialog>();
        private DialogType _shown;

        public void Show(DialogType type)
        {
            Debug.Log("Showing dialog " + type);
            if (_dialogs.TryGetValue(_shown, out var shownDialog))
                shownDialog.Close();
            _shown = type;
            if (_dialogs.TryGetValue(_shown, out var newDialog))
                newDialog.Show();
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Debug.Log(_gameOverDialog);
                _dialogs.Add(DialogType.GameOver, _gameOverDialog);
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            
        }
    }
}
