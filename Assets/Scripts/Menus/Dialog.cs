﻿using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Menus
{
    public class Dialog : MonoBehaviour
    {
        [SerializeReference]
        private BaseListener _listener;
        [SerializeField]
        private DialogType _type;
        public DialogType Type => _type;

        [SerializeField]
        private UnityEvent _shownHidden;

        private CanvasGroup _canvasGroup;

        internal void SetShown<T>(bool isShown, T parameter)
        {
            _listener?.UseParameter(parameter);
            _canvasGroup.alpha = isShown ? 1f : 0f;
            _canvasGroup.blocksRaycasts = isShown;
            _canvasGroup.interactable = isShown;
            _shownHidden.Invoke();
        }

        private void Awake()
        {
            _canvasGroup = GetComponentInChildren<CanvasGroup>();
        }
    }
}
