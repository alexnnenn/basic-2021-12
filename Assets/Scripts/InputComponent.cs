using Assets.Scripts.Input;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    internal class InputComponent : MonoBehaviour, IInputProvider
    {
        private MouseInputEvent _lastEvent;

        public MouseInputEvent ConsumeLastMouseEvent()
        {
            var result = _lastEvent;
            _lastEvent = null;
            return result;
        }

        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown((int)MouseButton.Left))
            {
                _lastEvent = new MouseInputEvent
                {
                    Button = MouseButton.Left,
                    X = UnityEngine.Input.mousePosition.x,
                    Y = UnityEngine.Input.mousePosition.y,
                };
            }
        }
    }
}
