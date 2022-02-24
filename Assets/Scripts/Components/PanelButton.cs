using System;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class PanelButton : MonoBehaviour
    {
        internal event Action<PanelButton> Clicked;

        protected virtual bool IsVisibleInternal() => false;

        public bool IsVisible => IsVisibleInternal();

        public void OnClick() => Clicked?.Invoke(this);

        internal void SetShown(bool isVisible) => gameObject.SetActive(isVisible);
    }
}
