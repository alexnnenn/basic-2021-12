using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum State
    {
        Idle,
        RunningToEnemy,
        RunningFromEnemy,
        BeginAttack,
        Attack,
        BeginShoot,
        Shoot,
        Death,
        Dead,
    }

    public enum Weapon
    {
        Pistol,
        Bat,
    }

    Animator animator;

    State _state;
    State state
    {
        get => _state;
        set
        {
            Debug.Log($"{this} -> {value}");
            _state = value;
        }
    }

    public Weapon weapon;
    public Character target;
    public float runSpeed;
    public float distanceFromEnemy;
    public int Health;
    public int Ammo;

    Vector3 originalPosition;
    Quaternion originalRotation;
    PistolMarker _pistol;
    bool _isAngry;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        _pistol = GetComponentInChildren<PistolMarker>();
        state = State.Idle;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public bool SetState(State newState)
    {
        if (state == State.Dead) //Трупы не оживают
            return false;

        if (state != State.Death || newState == State.Dead) //Умирают, но не выздоравливают
        {
            state = newState;
            return true;
        }

        return false;
    }

    public bool IsDead() => state == State.Dead || state == State.Death;

    private bool _isReady = true;
    public bool IsReady() => state == State.Idle && _isReady;

    [ContextMenu("Attack")]
    void AttackEnemy()
    {
        switch (weapon)
        {
            case Weapon.Pistol when Ammo > 0:
                SetState(State.BeginShoot);
                break;
            default:
                SetState(State.RunningToEnemy);
                break;
        }
    }

    bool RunTowards(Vector3 targetPosition, float distanceFromTarget)
    {
        Vector3 distance = targetPosition - transform.position;
        if (distance.magnitude < 0.00001f)
        {
            transform.position = targetPosition;
            return true;
        }

        Vector3 direction = distance.normalized;
        transform.rotation = Quaternion.LookRotation(direction);

        targetPosition -= direction * distanceFromTarget;
        distance = (targetPosition - transform.position);

        Vector3 step = direction * runSpeed;
        if (step.magnitude < distance.magnitude)
        {
            transform.position += step;
            return false;
        }

        transform.position = targetPosition;
        return true;
    }

    void FixedUpdate()
    {
        animator.SetInteger("Ammo", Ammo);
        if (_pistol != null)
        {
            _pistol.gameObject.SetActive(Ammo > 0);
        }
        if (target.IsDead())
        {
            _isAngry = false;
        }

        switch (state)
        {
            case State.Idle:
                animator.SetBool("IsAngry", _isAngry);
                transform.rotation = originalRotation;
                animator.SetFloat("Speed", 0.0f);
                if (_isAngry && target.IsReady())
                {
                    AttackEnemy();
                }
                break;

            case State.RunningToEnemy when target.IsDead():
                state = State.RunningFromEnemy;
                break;

            case State.RunningToEnemy when !target.IsDead():
                animator.SetFloat("Speed", runSpeed);
                if (RunTowards(target.transform.position, distanceFromEnemy))
                    state = State.BeginAttack;
                break;

            case State.RunningFromEnemy:
                animator.SetFloat("Speed", runSpeed);
                if (RunTowards(originalPosition, 0.0f))
                    state = State.Idle;
                break;

            case State.BeginAttack:
                Debug.Log($"{this}: BeginAttack with {Ammo} ammo");
                _isReady = true;
                animator.SetTrigger("MeleeAttack");
                state = State.Attack;
                break;

            case State.Attack:
                break;

            case State.BeginShoot:
                _isReady = true;
                animator.SetTrigger("Shoot");
                state = State.Shoot;
                break;

            case State.Shoot:
                break;

            case State.Death:
                animator.SetTrigger("Death");
                break;

            case State.Dead:
                animator.SetBool("IsDead", true);
                break;
        }
    }

    internal void Damage(int value)
    {
        _isReady = false;
        Health -= value;
        if (Health <= 0)
        {
            Health = 0;
            SetState(State.Death);
        }
        else
        {
            _isAngry = true;
        }
    }
}
