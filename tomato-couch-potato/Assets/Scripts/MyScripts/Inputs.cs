using UnityEngine;

namespace trrne.Teeth
{
    public static class Inputs
    {
        public static bool Down()
        {
            return Input.anyKeyDown;
        }

        public static bool Down(int click)
        {
            return Input.GetMouseButtonDown(click);
        }

        public static bool Down(KeyCode key)
        {
            return Input.GetKeyDown(key);
        }

        public static bool Down(string name)
        {
            return Input.GetButtonDown(name);
        }

        public static bool Pressed()
        {
            return Input.anyKey;
        }

        public static bool Pressed(int click)
        {
            return Input.GetMouseButton(click);
        }

        public static bool Pressed(KeyCode key)
        {
            return Input.GetKey(key);
        }

        public static bool Pressed(string name)
        {
            return Input.GetButton(name);
        }

        public static bool Released(int click)
        {
            return Input.GetMouseButtonUp(click);
        }

        public static bool Released(KeyCode key)
        {
            return Input.GetKeyUp(key);
        }

        public static bool Released(string name)
        {
            return Input.GetButtonUp(name);
        }

        public static Vector2 Axis()
        {
            return new(Input.GetAxis(Constant.Keys.Horizontal), Input.GetAxis(Constant.Keys.Vertical));
        }

        public static Vector2 Axis(string x, string y)
        {
            return new(Input.GetAxis(x), Input.GetAxis(y));
        }

        public static Vector2 AxisRaw()
        {
            return new(Input.GetAxisRaw(Constant.Keys.Horizontal), Input.GetAxisRaw(Constant.Keys.Vertical));
        }

        public static Vector2 AxisRaw(string x, string y)
        {
            return new(Input.GetAxisRaw(x), Input.GetAxisRaw(y));
        }

    }
}
