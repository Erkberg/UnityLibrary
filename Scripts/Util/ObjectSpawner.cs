using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ErksUnityLibrary
{
    public class ObjectSpawner : MonoBehaviour
    {
        public bool spawnOnAwake;
        public SpawnModule masterModule;
        public List<SpawnModule> modules;

        private void Awake()
        {
            if(spawnOnAwake)
            {
                SpawnAll();
            }
        }

        [ContextMenu("Spawn all")]
        public void SpawnAll()
        {
            foreach(SpawnModule module in modules)
            {
                module.SpawnObjects();
            }
        }

        [ContextMenu("Destroy all")]
        public void DestroyAll()
        {
            foreach (SpawnModule module in modules)
            {
                module.DestroyObjects();
            }
        }

        [ContextMenu("Spawn selected")]
        public void SpawnSelected()
        {
            foreach (SpawnModule module in modules)
            {
                if(module.selected)
                {
                    module.SpawnObjects();
                }                
            }
        }

        [ContextMenu("Destroy selected")]
        public void DestroySelected()
        {
            foreach (SpawnModule module in modules)
            {
                if (module.selected)
                {
                    module.DestroyObjects();
                }
            }
        }

        [System.Serializable]
        public class SpawnModule
        {
            public bool selected;
            public bool linkPrefab;
            public bool addParallax;
            public float parallaxFactorForPositionZ = 100f;
            public Vector3 parallaxLoopOffset;
            public Transform holder;
            public List<Transform> prefabs;
            public int amount;
            public Vector3 minPosition, maxPosition;
            public bool randomizeAngleY;
            public float maxScaleDeviation;
            public bool scaleWithPositionZ;
            public float scaleFactorZ;

            public void SpawnObjects()
            {
                for (int i = 0; i < amount; i++)
                {
                    Transform element = null;
                    
                    if(linkPrefab)
                    {
#if UNITY_EDITOR
                        element = (Transform)PrefabUtility.InstantiatePrefab(prefabs.GetRandomItem(), holder);
#endif
                    }
                    else
                    {
                        element = Instantiate(prefabs.GetRandomItem(), holder);
                    }
                    
                    if(element)
                    {
                        element.position = GetRandomPosition();
                        Quaternion rot = randomizeAngleY ? Quaternion.Euler(0f, Random.Range(0f, 359f), 0f) : Quaternion.identity;
                        element.rotation = rot;
                        element.localScale *= Random.Range(1f - maxScaleDeviation, 1f + maxScaleDeviation);

                        if (scaleWithPositionZ)
                        {
                            float scaleZ = 1f - element.position.z / scaleFactorZ;
                            if (scaleZ < 0.01f)
                            {
                                scaleZ = 0.01f;
                            }
                            element.localScale *= scaleZ;
                        }

                        if (addParallax)
                        {
                            ParallaxObject parallax = element.gameObject.AddComponent<ParallaxObject>();
                            parallax.speed.x = -element.position.z / parallaxFactorForPositionZ;

                            if (parallaxLoopOffset != Vector3.zero)
                            {
                                parallax.looping = true;
                                parallax.loopOffset = parallaxLoopOffset;
                            }
                        }
                    }                    
                }
            }

            public void DestroyObjects()
            {
                Transform[] children = holder.GetComponentsInChildren<Transform>();

                for (int i = 0; i < children.Length; i++)
                {
                    if (children[i] != holder && children[i] != null)
                    {
                        DestroyImmediate(children[i].gameObject);
                    }
                }
            }

            private Vector3 GetRandomPosition()
            {
                return new Vector3(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y), Random.Range(minPosition.z, maxPosition.z));
            }
        }
    }
}