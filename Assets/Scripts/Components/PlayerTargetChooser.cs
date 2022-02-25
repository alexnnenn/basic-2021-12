using Assets.Scripts.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class PlayerTargetChooser : MonoBehaviour, ITargetChooser
    {
        [SerializeReference]
        private SkillsPanelComponent _skills;
        private CharacterSelectionSystem _selector;

        public bool IsPC => true;
        private Coroutine _chooseCoroutine;

        private IWeapon _choosedWeapon = null;
        private int _targetIndex;
        private List<IActor> _enemies;

        private void Start()
        {
            _selector = GetComponent<CharacterSelectionSystem>();
            _selector.CharacterSelected += OnCharacterSelected;
            _skills.WeaponChoosing += OnWeaponChoosing;
            _skills.NextTargetSwitching += OnNextTarget;
        }

        private void OnCharacterSelected(Character character)
        {
            var characterIndex = _enemies.IndexOf(character);
            if (characterIndex >= 0)
                _targetIndex = characterIndex;
        }

        private void OnNextTarget()
        {
            ChooseNextTarget();
        }

        private void OnWeaponChoosing(WeaponType weapon)
        {
            _choosedWeapon = weapon;
        }

        public void Choose(IActor actor, List<IActor> enemies, Action<IActor, IWeapon> callback)
        {
            if(_chooseCoroutine != null)
            {
                StopCoroutine(_chooseCoroutine);
            }
            _chooseCoroutine = StartCoroutine(ChooseInternal(actor, enemies, callback));
        }

        private IEnumerator ChooseInternal(IActor actor, List<IActor> enemies, Action<IActor, IWeapon> callback)
        {
            _enemies = enemies;
            _targetIndex = -1;
            ChooseNextTarget();
            _choosedWeapon = null;
            yield return new WaitWhile(() => _choosedWeapon == null);
            callback(_enemies[_targetIndex], _choosedWeapon);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                ChooseNextTarget();
            }
        }

        private void ChooseNextTarget()
        {
            _targetIndex = (_targetIndex + 1) % _enemies.Count;
            _selector.StartSelection(_enemies[_targetIndex] as Character, true);
        }
    }
}
