using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ErksUnityLibrary
{
    public class ScreenshotTool : MonoBehaviour
    {
        public KeyCode screenshotKey = KeyCode.F8;
        public string filename = "Screeny";
        public int superSize = 1;
        public bool addTimeString = true;

        private const string fileEnding = ".png";

        private void Update()
        {
            if (Input.GetKeyDown(screenshotKey))
            {
                string screenyName = filename;

                if (addTimeString)
                    screenyName += "_" + Regex.Replace(System.DateTime.Now.TimeOfDay.ToString().Split('.')[0], ":", "-");

                screenyName += fileEnding;

                ScreenCapture.CaptureScreenshot(screenyName, 1);
                Debug.Log("Captured screenshot of name " + screenyName);
            }
        }
    }
}