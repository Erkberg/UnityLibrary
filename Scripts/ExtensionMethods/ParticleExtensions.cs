using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public static class ParticleExtensions
    {
        public static void SetEmissionEnabled(this ParticleSystem particles, bool enabled)
        {
            if (particles)
            {
                var em = particles.emission;
                em.enabled = enabled;
            }
            else
            {
                Debug.Log("no particles object");
            }
        }

        public static void SetEmissionOverTime(this ParticleSystem particles, float value)
        {
            if (particles)
            {
                var em = particles.emission;
                em.rateOverTime = value;
            }
            else
            {
                Debug.Log("no particles object");
            }
        }
        
        public static void AddToEmissionOverTime(this ParticleSystem particles, float valueToAdd)
        {
            if (particles)
            {
                var em = particles.emission;
                float rate = em.rateOverTime.constant + valueToAdd;
                em.rateOverTime = rate;
            }
            else
            {
                Debug.Log("no particles object");
            }
        }

        public static void SetLifetime(this ParticleSystem particles, float value)
        {
            if (particles)
            {
                var main = particles.main;
                main.startLifetime = value;
            }
            else
            {
                Debug.Log("no particles object");
            }
        }
        
        public static void AddToLifetime(this ParticleSystem particles, float valueToAdd)
        {
            if (particles)
            {
                var main = particles.main;
                float lifetime = main.startLifetime.constant + valueToAdd;
                main.startLifetime = lifetime;
            }
            else
            {
                Debug.Log("no particles object");
            }
        }

        public static void SetStartSize(this ParticleSystem particles, float value)
        {
            if (particles)
            {
                var main = particles.main;
                main.startSize = value;
            }
            else
            {
                Debug.Log("no particles object");
            }
        }

        public static void SetStartColor(this ParticleSystem particles, Color color)
        {
            if (particles)
            {
                var main = particles.main;
                main.startColor = color;
            }
            else
            {
                Debug.Log("no particles object");
            }
        }

        public static Vector3 GetFirstParticleCollision(this ParticleSystem particles, GameObject other)
        {
            Vector3 position = Vector3.zero;
            List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
            int numCollisionEvents = particles.GetCollisionEvents(other, collisionEvents);

            int i = 0;

            if (numCollisionEvents > 0)
            {
                position = collisionEvents[i].intersection;
            }

            return position;
        }
    }
}