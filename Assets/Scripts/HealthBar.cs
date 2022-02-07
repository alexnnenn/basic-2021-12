using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private List<Image> _hearts = new List<Image>();
    private Character _char;
    private int _shownHealth;

    void Start()
    {
        _char = GetComponentInParent<Character>();
        _hearts.Add(transform.GetChild(0).GetComponent<Image>());
        _hearts.Add(transform.GetChild(1).GetComponent<Image>());
        _hearts.Add(transform.GetChild(2).GetComponent<Image>());
    }

    // Update is called once per frame
    void Update()
    {
        if (_char.Health != _shownHealth)
        {
            _shownHealth = _char.Health;
            for (var i = 0; i < 3; i++)
                _hearts[i].gameObject.SetActive(_shownHealth > i);
        }
    }
}
