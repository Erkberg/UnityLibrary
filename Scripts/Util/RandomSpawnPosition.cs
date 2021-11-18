using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public static class RandomSpawnPosition
    {
        public static Vector3 GetRandomSpawnPosition3D(float spawnX, float spawnZ, float spawnY = 0f)
        {
            Vector3 spawnPosi = Vector3.zero;
            spawnPosi.y = spawnY;

            float random = Random.Range(0f, 1f);

            if (random < 0.25f)
            {
                spawnPosi.x = spawnX;
                spawnPosi.z = Random.Range(-spawnZ, spawnZ);
            }
            else
            {
                if (random < 0.5f)
                {
                    spawnPosi.x = -spawnX;
                    spawnPosi.z = Random.Range(-spawnZ, spawnZ);
                }
                else
                {
                    if (random < 0.75f)
                    {
                        spawnPosi.x = Random.Range(-spawnX, spawnX);
                        spawnPosi.z = spawnZ;
                    }
                    else
                    {
                        spawnPosi.x = Random.Range(-spawnX, spawnX);
                        spawnPosi.z = -spawnZ;
                    }
                }
            }

            return spawnPosi;
        }

        public static Vector2 GetRandomSpawnPosition2D(float spawnX, float spawnY)
        {
            Vector2 spawnPosi = Vector2.zero;

            float random = Random.Range(0f, 1f);

            if (random < 0.25f)
            {
                spawnPosi.x = spawnX;
                spawnPosi.y = Random.Range(-spawnY, spawnY);
            }
            else
            {
                if (random < 0.5f)
                {
                    spawnPosi.x = -spawnX;
                    spawnPosi.y = Random.Range(-spawnY, spawnY);
                }
                else
                {
                    if (random < 0.75f)
                    {
                        spawnPosi.x = Random.Range(-spawnX, spawnX);
                        spawnPosi.y = spawnY;
                    }
                    else
                    {
                        spawnPosi.x = Random.Range(-spawnX, spawnX);
                        spawnPosi.y = -spawnY;
                    }
                }
            }

            return spawnPosi;
        }
    }
}