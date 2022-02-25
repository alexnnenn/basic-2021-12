using Assets.Scripts.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Components
{
    public class SkillsPanelComponent : MonoBehaviour
    {
        internal event Action<WeaponType> WeaponChoosing;
        internal event Action NextTargetSwitching;

        private List<PanelButton> _buttons = new List<PanelButton>();

        private void Awake()
        {
            _buttons.AddRange(GetComponentsInChildren<PanelUseWeaponButton>());
            foreach (var weapon in _buttons)
            {
                weapon.SetShown(false);
                weapon.Clicked += OnAttack;
            }

            var nextButton = GetComponentInChildren<PanelNextEnemyButton>();
            nextButton.Clicked += OnNextTarget;
            _buttons.Add(nextButton);
            nextButton.SetShown(false);
        }

        private void OnNextTarget(PanelButton btn)
        {
            NextTargetSwitching?.Invoke();
        }

        private void OnAttack(PanelButton btn)
        {
            var weapon = btn as PanelUseWeaponButton;
            if (weapon.IsUsable)
                WeaponChoosing?.Invoke(weapon.Weapon);
        }
        public void SetForActor(IActor actor)
        {
            gameObject.SetActive(actor.Team.Controller.IsPC);
            foreach (var button in _buttons)
            {
                if (button is PanelUseWeaponButton weapon)
                    weapon.SetItems(actor.Items);
                button.SetShown(button.IsVisible);
            }
        }
    }
}
