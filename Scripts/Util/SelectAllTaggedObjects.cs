#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ErksUnityLibrary
{
    public static class SelectAllTaggedObjects
    {
        private static string UntaggedTag = "Untagged";
 
        [MenuItem("Tools/ErksUnityLibrary/Select all tagged objects")]
        public static void SelectObjectsWithTag()
        {
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
            List<GameObject> taggedObjects = new List<GameObject>();

            foreach(GameObject go in allObjects)
            {
                if(!go.CompareTag(UntaggedTag))
                {
                    taggedObjects.Add(go);
                }
            }

            Selection.objects = taggedObjects.ToArray();
        }
    }
}
#endif