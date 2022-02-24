using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Components
{
    public sealed class AmmoTextComponent : MonoBehaviour
    {
        private Text _text;

        private int? _ammo;
        public int? Ammo
        {
            get => _ammo;
            set
            {
                _ammo = value;
                _text.text = Ammo == null ? string.Empty : Ammo.ToString();
            }
        }

        private void Awake()
        {
            _text = GetComponent<Text>();
        }
    }
}
