using Assets.Scripts.Logic;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerMoveComponent : MonoBehaviour, IChooseTarget
    {
        public void Choose(Action<IActor> chooseCallback)
        {
            StartCoroutine(ChooseEnemy(chooseCallback));
        }

        private IEnumerator ChooseEnemy(Action<IActor> chooseCallback)
        {
            while (true)
            {
                while (!Input.GetMouseButtonDown(0))
                    yield return new WaitForFixedUpdate();

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out var hit) && hit.collider.GetComponent<CharacterComponent>() is IActor actor)
                {
                    chooseCallback(actor);
                    yield break;
                }
            }
        }
    }
}
