using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    internal class RoundNumberComponent : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponentInChildren<Text>(true);
        }

        internal IEnumerator ShowRound(int number)
        {
            _text.text = $"Round {number}";
            yield return new WaitForSeconds(1f);
            _text.text = string.Empty;
        }
    }
}
