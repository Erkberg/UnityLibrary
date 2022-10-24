using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ErksUnityLibrary
{
    public static class FileLocationOpener
    {
        [MenuItem("UnityLibrary/Open persistent data path")]
        public static void OpenPersistentDataPath()
        {
            EditorUtility.RevealInFinder(Application.persistentDataPath);
        }
    }
}
