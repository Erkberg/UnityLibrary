using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            public Transform holder;
            public List<Transform> prefabs;
            public int amount;
            public Vector3 minPosition, maxPosition;
            public bool randomizeAngleY;
            public float maxScaleDeviation;

            public void SpawnObjects()
            {
                for (int i = 0; i < amount; i++)
                {
                    Quaternion rot = Quaternion.Euler(0f, Random.Range(0f, 359f), 0f);
                    Transform element = Instantiate(prefabs.GetRandomItem(), GetRandomPosition(), 
                        randomizeAngleY ? rot : Quaternion.identity, holder);
                    element.localScale *= Random.Range(1f - maxScaleDeviation, 1f + maxScaleDeviation);
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