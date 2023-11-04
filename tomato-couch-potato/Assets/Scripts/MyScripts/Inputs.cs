using System.Linq;
using UnityEngine;

namespace trrne.Box
{
    public static class Inputs
    {
        public static bool Down() => Input.anyKeyDown;
        public static bool Down(int click) => Input.GetMouseButtonDown(click);
        public static bool Down(KeyCode key) => Input.GetKeyDown(key);
        public static bool Down(string name) => Input.GetButtonDown(name);
        public static bool DownOR(params string[] names) => (from n in names where Down(n) select n).ToArray().Length >= 1;
        public static bool DownAND(params string[] names) => (from n in names where Down(n) select n).ToArray().Length == names.Length;

        public static bool Pressed() => Input.anyKey;
        public static bool Pressed(int click) => Input.GetMouseButton(click);
        public static bool Pressed(KeyCode key) => Input.GetKey(key);
        public static bool Pressed(string name) => Input.GetButton(name);
        public static bool PressedOR(params string[] names) => (from n in names where Pressed(n) select n).ToArray().Length >= 1;
        public static bool PressedAND(params string[] names) => (from n in names where Pressed(n) select n).ToArray().Length == names.Length;

        public static bool Released(int click) => Input.GetMouseButtonUp(click);
        public static bool Released(KeyCode key) => Input.GetKeyUp(key);
        public static bool Released(string name) => Input.GetButtonUp(name);
        public static bool ReleasedOR(params string[] names) => (from n in names where Released(n) select n).ToArray().Length >= 1;
        public static bool ReleasedAND(params string[] names) => (from n in names where Released(n) select n).ToArray().Length == names.Length;

        public static Vector2 Axis() => new(Input.GetAxis(Constant.Keys.Horizontal), Input.GetAxis(Constant.Keys.Vertical));
        public static Vector2 Axis(string x, string y) => new(Input.GetAxis(x), Input.GetAxis(y));
        public static Vector2 AxisRaw() => new(Input.GetAxisRaw(Constant.Keys.Horizontal), Input.GetAxisRaw(Constant.Keys.Vertical));
        public static Vector2 AxisRaw(string x, string y) => new(Input.GetAxisRaw(x), Input.GetAxisRaw(y));

    }
}
