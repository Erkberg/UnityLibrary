using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public static class ParticleExtensions
    {
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