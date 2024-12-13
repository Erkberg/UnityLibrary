using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public class CooldownComponent : MonoBehaviour
    {
        public Action onCooldownPassed;

        [SerializeField] private float cooldown;
        [SerializeField] private bool autoRestart;

        private float cooldownPassed;
        private bool isOnCooldown;
        private bool paused;

        private void Update()
        {
            HandleCooldown();
        }

        private void HandleCooldown()
        {
            if (isOnCooldown && !paused)
            {
                cooldownPassed += Time.deltaTime;
                if (cooldownPassed >= cooldown)
                {                    
                    ResetCooldown();
                    onCooldownPassed?.Invoke();
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

            if(!autoRestart)
            {
                isOnCooldown = false;
            }            
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

        public void SetPaused(bool paused)
        { 
            this.paused = paused;
        }
    }
}