using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public class PlayerController2D : MonoBehaviour
    {
        [Header("Refs")]
        public Rigidbody2D rb2D;
        public GroundChecker groundChecker;

        [Header("Input")] 
        public string horizontalInputAxis = "Horizontal";
        public string jumpButton = "Jump";

        [Header("Config")] 
        public float moveSpeed = 4f;
        public float jumpStrength = 6f;
        public bool applyExtraGravity = true;
        public float fallMultiplier = 2.5f;
        public float lowJumpMultiplier = 2f;

        protected virtual float GetHorizontalAxis()
        {
            return Input.GetAxis(horizontalInputAxis);
        }

        protected virtual bool GetJumpButtonDown()
        {
            return Input.GetButtonDown(jumpButton);
        }

        protected virtual bool GetJumpButton()
        {
            return Input.GetButton(jumpButton);
        }

        private void Update()
        {
            HorizontalMovement();
            VerticalMovement();
        }

        private void HorizontalMovement()
        {
            rb2D.SetVelocityX(GetHorizontalAxis() * moveSpeed);
        }

        private void VerticalMovement()
        {
            if (GetJumpButtonDown() && groundChecker.isGrounded)
            {
                rb2D.SetVelocityY(jumpStrength);
            }

            if (applyExtraGravity)
            {
                if (rb2D.velocity.y < 0f)
                {
                    ApplyExtraGravity(fallMultiplier);
                }
                else if (rb2D.velocity.y > 0f && !GetJumpButton())
                {
                    ApplyExtraGravity(lowJumpMultiplier);
                }
            }
        }

        private void ApplyExtraGravity(float multiplier)
        {
            rb2D.AddVelocityY(Physics2D.gravity.y * (multiplier - 1f) * Time.deltaTime);
        }
    }
}