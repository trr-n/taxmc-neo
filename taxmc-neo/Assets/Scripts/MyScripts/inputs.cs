using UnityEngine;

namespace trrne.Bag
{
    public static class Inputs
    {
        // public static bool down => Input.anyKeyDown;
        public static bool Down() => Input.anyKeyDown;
        public static bool Down(int click) => Input.GetMouseButtonDown(click);
        public static bool Down(KeyCode key) => Input.GetKeyDown(key);
        public static bool Down(string name) => Input.GetButtonDown(name);

        // public static bool pressed => Input.anyKey;
        public static bool Pressed() => Input.anyKey;
        public static bool Pressed(int click) => Input.GetMouseButton(click);
        public static bool Pressed(KeyCode key) => Input.GetKey(key);
        public static bool Pressed(string name) => Input.GetButton(name);

        public static bool Released(int click) => Input.GetMouseButtonUp(click);
        public static bool Released(KeyCode key) => Input.GetKeyUp(key);
        public static bool Released(string name) => Input.GetButtonUp(name);
    }
}