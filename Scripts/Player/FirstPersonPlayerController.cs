using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public class FirstPersonPlayerController : MonoBehaviour
    {
        [Header("State")]
        public bool moveEnabled = true;
        public bool jumpEnabled = true;
        public bool lookEnabled = true;

        [Header("Refs")]
        [SerializeField] protected CharacterController cc;
        [SerializeField] protected GroundChecker groundChecker;
        [SerializeField] protected Transform cam;

        [Header("Input")]
        [SerializeField] protected string horizontalInputAxis = "Horizontal";
        [SerializeField] protected string verticalInputAxis = "Vertical";
        [SerializeField] protected string jumpButton = "Jump";
        [SerializeField] protected string horizontalMouseAxis = "Mouse X";
        [SerializeField] protected string verticalMouseAxis = "Mouse Y";

        [Header("Move")]        
        [SerializeField] protected float moveSpeed = 4;
        [SerializeField] protected float accelerationFloor = 32;
        [SerializeField] protected float accelerationAir = 8;
        [SerializeField] protected float decelerationFloor = 32;
        [SerializeField] protected float decelerationAir = 8;

        [Header("Jump")]
        [SerializeField] protected float jumpHeight = 1;
        [SerializeField] protected bool applyExtraGravity = true;
        [SerializeField] protected float fallMultiplier = 2.5f;
        [SerializeField] protected float lowJumpMultiplier = 2;
        [SerializeField] protected float jumpVelocityMultiplier = 1.2f;

        [Header("Look")]
        [SerializeField] protected float maxCamAngle = 86;
        [SerializeField] protected float mouseSensitivity = 1f;
        [SerializeField] protected bool lockMouse = true;

        protected float gravity;
        protected float jumpStrength;
        protected float vertRot = 0f;
        protected float horRot = 0f;
        protected Vector2 mouseMotion;
        protected Vector3 currentVelocity;
        protected Vector3 previousVelocity;

        protected virtual float GetHorizontalAxis()
        {
            return Input.GetAxis(horizontalInputAxis);
        }

        protected virtual float GetVerticalAxis()
        {
            return Input.GetAxis(verticalInputAxis);
        }

        protected virtual bool GetJumpButtonDown()
        {
            return Input.GetButtonDown(jumpButton);
        }

        protected virtual bool GetJumpButton()
        {
            return Input.GetButton(jumpButton);
        }

        protected virtual void UpdateMouseMotion()
        {
            mouseMotion = new Vector2(Input.GetAxis(horizontalMouseAxis), Input.GetAxis(verticalMouseAxis));
        }

        private void Awake()
        {
            gravity = Mathf.Abs(Physics.gravity.y);
            jumpStrength = GetJumpStrengthFromHeight(jumpHeight);

            if (lockMouse)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        private void Update()
        {
            UpdateMouseMotion();
            Gravity();

            if(jumpEnabled)
                Jump();

            if(moveEnabled)
                Move();

            if(lookEnabled)
                Look();

            cc.Move(currentVelocity * Time.deltaTime);
            previousVelocity = cc.velocity;
        }

        protected virtual void Move()
        {
            Vector3 velocity = cam.forward * GetVerticalAxis() + cam.right * GetHorizontalAxis();
            velocity *= moveSpeed;
            velocity.y = currentVelocity.y;

            currentVelocity = velocity;
        }

        protected virtual void Jump()
        {
            
        }

        protected virtual void DoJump()
        {
            Vector3 velocity = currentVelocity;
            velocity *= jumpVelocityMultiplier;
            velocity.y = jumpStrength;
            currentVelocity = velocity;
        }

        protected virtual void Gravity()
        {
            Vector3 velocity = currentVelocity;

            if (!groundChecker.isGrounded)
            {
                if (velocity.y >= 0)
                {
                    velocity.y -= gravity * Time.deltaTime;
                }
                else
                {
                    velocity.y -= gravity * Time.deltaTime * fallMultiplier;
                }
            }
            else
            {
                velocity.y = 0;
            }

            currentVelocity = velocity;
        }

        protected virtual void Look()
        {
            horRot += mouseMotion.x * mouseSensitivity;
            vertRot -= mouseMotion.y * mouseSensitivity;
            vertRot = Mathf.Clamp(vertRot, -maxCamAngle, maxCamAngle);

            cam.localRotation = Quaternion.Euler(vertRot, horRot, 0f);
        }

        protected float GetJumpStrengthFromHeight(float jumpHeight)
        {
            return Mathf.Sqrt(jumpHeight * 2 * gravity);
        }
    }
}

