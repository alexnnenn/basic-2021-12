using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    internal class UIComponent : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponentInChildren<Text>(true);
            Debug.Log($"UI awaked and text is {_text}");
        }

        internal IEnumerator ShowRound(int number)
        {
            Debug.Log($"{this} Showing round {number}");
            _text.text = $"Round {number}";
            _text.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            _text.gameObject.SetActive(false);
        }
    }
}
