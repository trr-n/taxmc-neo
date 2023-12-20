using System.Linq;
using UnityEngine;
// using UnityEngine.InputSystem;

namespace trrne.Box
{
    public static class Inputs
    {
        public static bool down => Input.anyKeyDown;
        public static bool Down() => down;
        public static bool Down(int click) => Input.GetMouseButtonDown(click);
        public static bool Down(KeyCode key) => Input.GetKeyDown(key);
        public static bool Down(string name) => Input.GetButtonDown(name);
        public static bool DownOr(params string[] names)
        => (from n in names where Down(n) select n).Count() >= 1;
        public static bool DownAnd(params string[] names)
        => (from n in names where Down(n) select n).Count() == names.Length;

        public static bool pressed => Input.anyKey;
        public static bool Pressed() => pressed;
        public static bool Pressed(int click) => Input.GetMouseButton(click);
        public static bool Pressed(KeyCode key) => Input.GetKey(key);
        public static bool Pressed(string name) => Input.GetButton(name);
        public static bool PressedOr(params string[] names)
        => (from n in names where Pressed(n) select n).Count() >= 1;
        public static bool PressedAnd(params string[] names)
        => (from n in names where Pressed(n) select n).Count() == names.Length;

        public static bool Up(int click) => Input.GetMouseButtonUp(click);
        public static bool Up(KeyCode key) => Input.GetKeyUp(key);
        public static bool Up(string name) => Input.GetButtonUp(name);
        public static bool UpOr(params string[] names)
        => (from n in names where Up(n) select n).Count() >= 1;
        public static bool UpAnd(params string[] names)
        => (from n in names where Up(n) select n).Count() == names.Length;

        public static Vector2 Axis() => new(Input.GetAxis(Config.Keys.HORIZONTAL), Input.GetAxis(Config.Keys.VERTICAL));
        public static Vector2 Axis(string x, string y) => new(Input.GetAxis(x), Input.GetAxis(y));
        public static Vector2 AxisRaw() => new(Input.GetAxisRaw(Config.Keys.HORIZONTAL), Input.GetAxisRaw(Config.Keys.VERTICAL));
        public static Vector2 AxisRaw(string x, string y) => new(Input.GetAxisRaw(x), Input.GetAxisRaw(y));

        // public static bool Down(Key key) => Keyboard.current[key].wasPressedThisFrame;
        // public static bool DownOr(params Key[] keys) => (from k in keys where Down(k) select k).Count() >= 1;
        // public static bool DownAnd(params Key[] keys) => (from k in keys where Down(k) select k).Count() == keys.Length;

        // public static bool Pressed(Key key) => Keyboard.current[key].isPressed;
        // public static bool PressedOr(params Key[] keys) => (from k in keys where Pressed(k) select k).Count() >= 1;
        // public static bool PressedAnd(params Key[] keys) => (from k in keys where Pressed(k) select k).Count() == keys.Length;

        // public static bool Up(Key key) => Keyboard.current[key].wasReleasedThisFrame;
        // public static bool UpOr(params Key[] keys) => (from k in keys where Up(k) select k).Count() >= 1;
        // public static bool UpAnd(params Key[] keys) => (from k in keys where Up(k) select k).Count() == keys.Length;
    }
}
