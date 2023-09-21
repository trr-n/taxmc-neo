using UnityEngine;
using static UnityEngine.Application;

namespace trrne.utils
{
    public enum FrameRate { Low = 30, Medium = 60, High = 144, Ultra = 200, VSync = -1 }
    public enum CursorAppearance { Invisible, Visible }
    public enum CursorRangeOfMotion { InScene = CursorLockMode.Confined, Fixed = CursorLockMode.Locked, Limitless = CursorLockMode.None }

    public class App
    {
        public static void SetFPS(int fps = -1) => targetFrameRate = fps;
        public static void SetFPS(FrameRate fps) => targetFrameRate = (int)fps;
        public static float fps => Mathf.Floor(1 / Time.deltaTime);

        public static void SetGravity(Vector3 gravity) => Physics2D.gravity = gravity;
        public static Vector2 gravity => Physics2D.gravity;

        public static void SetCursorStatus(CursorAppearance appear, CursorRangeOfMotion rangeOfMotion)
        {
            Cursor.visible = appear == CursorAppearance.Visible;
            Cursor.lockState = (CursorLockMode)rangeOfMotion;
        }

        public static float timeScale => Time.timeScale;
        public static bool TimeScale(float scale) => Time.timeScale == scale;

        public static string platform => Application.platform.ToString();

        public static Vector2 resolution => new(Screen.currentResolution.width, Screen.currentResolution.height);
        public static Vector2 screenSize => new(Screen.width, Screen.height);
    }
}