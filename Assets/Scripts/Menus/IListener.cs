using UnityEngine;

namespace Assets.Scripts.Menus
{
    internal class BaseListener : MonoBehaviour
    {
        protected object _parameter;

        internal void UseParameter(object parameter) => _parameter = parameter;
    }
}
