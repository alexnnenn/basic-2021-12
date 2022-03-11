using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D rigidbody2D;
        private Animator animator;
        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private int _jumpCounter;

        private void FixedUpdate()
        {
            CheckMaxSpeed();

            float hDirection = Input.GetAxis("Horizontal");

            if (hDirection < 0.0f && !_isLeftGrounded)
            {
                RunLeft();
            }
            else if (hDirection > 0.0f && !_isRightGrounded)
            {
                RunRight();
            }
            else if (_isFootGrounded)
            {
                Stop();
            }

            if (Input.GetKeyDown(KeyCode.Space) && CanJump())
            {
                Jump();
                if (!_isFootGrounded)
                {
                    if (_isLeftGrounded && !_isRightGrounded)
                    {
                        RunRight();
                    }
                    else if (!_isLeftGrounded && _isRightGrounded)
                    {
                        RunLeft();
                    }
                }
            }
        }

        private void CheckMaxSpeed()
        {
            if (rigidbody2D.velocity.x > 5)
                rigidbody2D.velocity = new Vector2(5, rigidbody2D.velocity.y);
            if (rigidbody2D.velocity.x < -5)
                rigidbody2D.velocity = new Vector2(-5, rigidbody2D.velocity.y);
        }

        private void RunRight()
        {
            rigidbody2D.AddForce(new Vector2(3, 0), ForceMode2D.Impulse);
            spriteRenderer.flipX = false;
            animator.SetBool("isRunning", true);
        }

        private void RunLeft()
        {
            rigidbody2D.AddForce(new Vector2(-3, 0), ForceMode2D.Impulse);
            spriteRenderer.flipX = true;
            animator.SetBool("isRunning", true);
        }

        private void Stop()
        {
            rigidbody2D.velocity = new Vector2(0f, rigidbody2D.velocity.y);
            animator.SetBool("isRunning", false);
        }

        private void Jump()
        {
            _jumpCounter++;
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 7f);
        }

        private bool CanJump()
        {
            return (_isFootGrounded || _isLeftGrounded || _isRightGrounded) && _jumpCounter < 2;
        }

        private bool _isFootGrounded;
        public void SetFootGrounded()
        {
            _isFootGrounded = true;
            _jumpCounter = 0;
        }

        public void SetFootUngrounded() => _isFootGrounded = false;

        private bool _isLeftGrounded;
        public void SetLeftGrounded() => _isLeftGrounded = true;
        public void SetLeftUngrounded() => _isLeftGrounded = false;

        private bool _isRightGrounded;
        public void SetRightGrounded() => _isRightGrounded = true;
        public void SetRightUngrounded() => _isRightGrounded = false;
    }
}