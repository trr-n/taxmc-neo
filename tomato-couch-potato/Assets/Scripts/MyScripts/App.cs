using System;
using UnityEngine;
using UniApp = UnityEngine.Application;

namespace trrne.Box
{
    public static class App
    {
        public static string DataPath(string name) => $"{UniApp.dataPath}/{name}";
        public static string PersistantPath(string name) => $"{UniApp.persistentDataPath}/{name}";

        /// <summary>
        /// FPSを設定する
        /// </summary>
        /// <param name="fps">FPS(int)</param>
        public static void SetFPS(int fps = -1) => UniApp.targetFrameRate = fps;

        /// <summary>
        /// FPSを設定する
        /// </summary>
        /// <param name="fps">FPS(enum)</param>
        public static void SetFPS(FrameRate fps) => UniApp.targetFrameRate = (int)fps;

        /// <summary>
        /// FPSを取得する
        /// </summary>
        /// <param name="digit">桁数指定</param>
        /// <returns>FPSを返す</returns>
        public static float FPS(int digit) => MathF.Round(1 / Time.unscaledDeltaTime, digit);

        /// <summary>
        /// FPSを取得する
        /// </summary>
        /// <returns>FPSを返す</returns>
        public static int fps => MF.Cutail(FPS(digit: 0));

        public static void SetCursorStatus(CursorAppearance appear, CursorRangeOfMotion rangeOfMotion)
        {
            Cursor.visible = appear == CursorAppearance.Visible;
            Cursor.lockState = (CursorLockMode)rangeOfMotion;
        }

        public static bool TimeScale(float scale, out float truth) => (truth = Time.timeScale) == scale;
        public static bool TimeScale(float scale) => Time.timeScale == scale;

        public static void SetTimeScale(float scale) => Time.timeScale = scale;

        public static RuntimePlatform Platform => UniApp.platform;

        public static bool IsPlatform(RuntimePlatform platform, out RuntimePlatform truth) => (truth = UniApp.platform) == platform;

        public static Vector2 GetScreenInfo(KindaScreen kinda)
        => kinda switch
        {
            KindaScreen.Resolution => new(Screen.currentResolution.width, Screen.currentResolution.height),
            KindaScreen.Size => new(Screen.width, Screen.height),
            KindaScreen.Center => GetScreenInfo(KindaScreen.Size) / 2,
            _ => throw null
        };

        public static void SetCameraSize(float size) => Camera.main.orthographicSize = size;

        public static float CameraSize => Camera.main.orthographicSize;
    }
}
