using UnityEngine;

namespace ErksUnityLibrary
{
	public static class ScreenUtils
	{
        public static bool IsMouseInsideScreen()
        {
            return Input.mousePosition.x >= 0f && Input.mousePosition.x <= Screen.width &&
                Input.mousePosition.y >= 0f && Input.mousePosition.y <= Screen.height;
        }

        public static Vector2 GetMousePositionScreenPercentage()
        {
            return new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);
        }

        public static Vector2 GetMouseDistancePercentageFromCenter()
        {
            return (GetMousePositionScreenPercentage() - Vector2.one / 2) * 2;
        }
    }
}