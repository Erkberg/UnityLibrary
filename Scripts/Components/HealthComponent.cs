using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public class HealthComponent : MonoBehaviour
    {
        public float maxHealth = 1;
        public float currentHealth = 1;

        public event Action died;
        public event Action healthChanged;
        public event Action healthIncreased;
        public event Action healthDecreased;

        public void Damage(float amount, bool forceCallEvents = false)
        {
            bool hasHealthDecreased = currentHealth > 0 && amount != 0;

            currentHealth -= amount;
            ClampCurrentHealth();

            if (hasHealthDecreased || forceCallEvents)
            {
                healthDecreased?.Invoke();
                healthChanged?.Invoke();
            }

            if (currentHealth <= 0)
            {
                died?.Invoke();
            }
        }

        public void Heal(float amount, bool forceCallEvents = false)
        {
            bool hasHealthIncreased = currentHealth < maxHealth && amount != 0;

            currentHealth += amount;
            ClampCurrentHealth();

            if (hasHealthIncreased || forceCallEvents)
            {
                healthIncreased?.Invoke();
                healthChanged?.Invoke();
            }
        }

        public float GetHealthPercentage()
        {
            return currentHealth / maxHealth;
        }

        private void ClampCurrentHealth()
        {
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        }
    }
}