using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public class CooldownComponent : MonoBehaviour
    {
        [SerializeField] private float cooldown;

        private float cooldownPassed;
        private bool isOnCooldown;

        private void Update()
        {
            HandleCooldown();
        }

        private void HandleCooldown()
        {
            if (isOnCooldown)
            {
                cooldownPassed += Time.deltaTime;
                if (cooldownPassed >= cooldown)
                {
                    ResetCooldown();
                }
            }
        }

        public void StartCooldown()
        {
            isOnCooldown = true;
        }

        public void ResetCooldown()
        {
            cooldownPassed = 0;
            isOnCooldown = false;
        }

        public void SetCooldown(float cooldown)
        {
            this.cooldown = cooldown;
        }

        public bool IsOnCooldown()
        {
            return isOnCooldown;
        }

        public float GetCooldownPassed()
        {
            return cooldownPassed;
        }

        public float GetCooldownPercentage()
        {
            return cooldownPassed / cooldown;
        }
    }
}