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
    }
}