using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Self
{
    public static class Constant
    {
        public readonly struct Scenes
        {
            public static string
            Title = "Title",
            Main = "BottleSort";
        }

        public readonly struct Layers
        {
            public const int
            Player = 1 << 6,
            Ground = 1 << 7;
        }

        public readonly struct Keys
        {
            public static string
            Horizontal = "Horizontal",
            Vertical = "Vertical",
            Jump = "Jump";
        }

        public readonly struct Tags
        {
            public static string
            Player = "Player";
        }
    }
}
