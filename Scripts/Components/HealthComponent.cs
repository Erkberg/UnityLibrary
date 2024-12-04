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

        public void Damage(float amount)
        {
            if (currentHealth > 0)
            {
                healthDecreased?.Invoke();
                healthChanged?.Invoke();
            }

            currentHealth -= amount;
            ClampCurrentHealth();

            if (currentHealth <= 0)
            {
                died?.Invoke();
            }
        }

        public void Heal(float amount)
        {
            if (currentHealth < maxHealth)
            {
                healthIncreased?.Invoke();
                healthChanged?.Invoke();
            }

            currentHealth += amount;
            ClampCurrentHealth();
        }

        public float GetHealthPercentage()
        {
            return currentHealth / maxHealth;
        }

        public void InvokeHealthChanged()
        {
            healthChanged?.Invoke();
        }

        private void ClampCurrentHealth()
        {
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        }
    }
}