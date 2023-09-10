using System.Collections;
using System.Collections.Generic;
using Self.Utils;
using UnityEngine;

namespace Self
{
    public static class Constant
    {
        public static Vector3 DefaultGravity = -Coordinate.Y * Coordinate.G;

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
            Ground = 1 << 7,
            Obstacle = 1 << 8;
        }

        public readonly struct Keys
        {
            public static string
            Horizontal = "Horizontal",
            Vertical = "Vertical",
            Jump = "Jump",
            Down = "Down";
        }

        public readonly struct Tags
        {
            public static string
            Player = "Player",
            Ladder = "Ladder",
            Pad = "Pad",
            MainCamera = "MainCamera",
            Enemy = "Enemy";
        }
    }
}
