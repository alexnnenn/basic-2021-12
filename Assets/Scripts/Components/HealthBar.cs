using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class HealthBar : MonoBehaviour
    {
        private List<Image> _hearts = new List<Image>();
        private HealthComponent _charHealth;

        private void Awake()
        {
            _charHealth = GetComponentInParent<HealthComponent>();
            for(var i = 0; i < transform.childCount; i++)
                _hearts.Add(transform.GetChild(i).GetComponent<Image>());
        }

        private void Start()
        {
            if(_charHealth != null)
                _charHealth.Changed += OnHealthChanged;
            OnHealthChanged(_charHealth.Health);
        }

        private void OnHealthChanged(int health)
        {
            for (var i = 0; i < _hearts.Count; i++)
                _hearts[i].gameObject.SetActive(health > i);
        }

        private void OnDestroy()
        {
            if (_charHealth != null)
                _charHealth.Changed -= OnHealthChanged;
        }
    }
}