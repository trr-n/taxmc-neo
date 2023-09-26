using trrne.Bag;
using UnityEngine;

namespace trrne
{
    public static partial class Fixed
    {
        public readonly struct Scenes
        {
            public static string
            Prefix = "Game",
            Title = "StageSelect";

            public static string[]
            Game = { Prefix + 1 };
        }

        public readonly struct Layers
        {
            public const int
            None = 1 << 0,
            Player = 1 << 6,
            Ground = 1 << 7,
            Object = 1 << 8,
            Entity = 1 << 9;
        }

        public readonly struct Keys
        {
            public static string
            Horizontal = "Horizontal",
            Vertical = "Vertical",
            Wheel = "Mouse ScrollWheel",
            Jump = "Jump",
            Down = "Down",
            Zoom = "Zoom";
        }

        public readonly struct Tags
        {
            public static string
            Player = "Player",
            Ladder = "Ladder",
            Pad = "Pad",
            MainCamera = "MainCamera",
            Enemy = "Enemy",
            Panel = "Panel",
            Ice = "Ice";
        }

        public readonly struct SpawnPositions
        {
            public static Vector2
            Stage1 = Coordinate.zero;
        }
    }
}
