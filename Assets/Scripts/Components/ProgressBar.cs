using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Components
{
    internal class ProgressBar : MonoBehaviour
    {
        private Image _image;
        private Text _text;

        private void Awake()
        {
            _text = GetComponentInChildren<Text>();
            _image = GetComponentInChildren<Image>();
        }

        internal void SetPercent(float percent)
        {
            _text.text = $"{(int)percent} %";
            _image.fillAmount = percent;
        }
    }
}
