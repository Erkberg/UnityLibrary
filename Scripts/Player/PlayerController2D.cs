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
        
        private void Update()
        {
            HorizontalMovement();
            CheckJump();
        }

        private void HorizontalMovement()
        {
            rb2D.SetVelocityX(Input.GetAxis(horizontalInputAxis) * moveSpeed);
        }

        private void CheckJump()
        {
            if (Input.GetButtonDown(jumpButton) && groundChecker.isGrounded)
            {
                rb2D.SetVelocityY(jumpStrength);
            }
        }
    }
}