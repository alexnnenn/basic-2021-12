using System;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class CharacterSelectionSystem : MonoBehaviour
    {
        [SerializeField]
        private GameObject _selectMarkPrototype;

        private bool _isSelectMode;
        private GameObject _selectMark;

        internal event Action<Character> CharacterSelected;

        internal void StopSelection()
        {
            _isSelectMode = false;
            EnsureMark();
            _selectMark.SetActive(false);
        }

        internal void StartSelection(Character character, bool allowManual)
        {
            _isSelectMode = allowManual;
            EnsureMark();
            _selectMark.SetActive(true);
            _selectMark.transform.position = character.transform.position + new Vector3(0, 3f, 0);
            if(allowManual)
                CharacterSelected?.Invoke(character);
        }

        private void Update()
        {
            if (!_isSelectMode)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit))
                {
                    var character = hit.collider.GetComponent<Character>();
                    if (character != null)
                    {
                        StartSelection(character, true);
                    }
                }
            }
        }

        private void EnsureMark()
        {
            _selectMark ??= Instantiate(_selectMarkPrototype);
        }
    }
}
