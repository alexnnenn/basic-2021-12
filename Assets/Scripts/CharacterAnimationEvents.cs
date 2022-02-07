using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationEvents : MonoBehaviour
{
    Character character;

    void Start()
    {
        character = GetComponentInParent<Character>();
    }

    void ShootEnded()
    {
        character.Ammo--;
        character.target.Damage(2);
        character.SetState(Character.State.Idle);
    }

    void AttackEnd()
    {
        character.target.Damage(1);
        character.SetState(Character.State.RunningFromEnemy);
    }

    void Dying()
    {
        character.SetState(Character.State.Dead);
    }
}
