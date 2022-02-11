using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class HealthBar : MonoBehaviour
    {
        private List<Image> _hearts = new List<Image>();
        private HealthComponent _char;
        private int _shownHealth;

        void Awake()
        {
            _char = GetComponentInParent<HealthComponent>();
            for(var i = 0; i < transform.childCount; i++)
                _hearts.Add(transform.GetChild(i).GetComponent<Image>());
        }

        void Update()
        {
            if (_char.Health != _shownHealth)
            {
                _shownHealth = _char.Health;
                for (var i = 0; i < _hearts.Count; i++)
                    _hearts[i].gameObject.SetActive(_shownHealth > i);
            }
        }
    }
}