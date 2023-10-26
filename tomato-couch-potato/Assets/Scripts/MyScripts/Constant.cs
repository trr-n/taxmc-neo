using trrne.Pancreas;
using UnityEngine;

namespace trrne
{
    public struct Constant
    {
        public readonly struct Scenes
        {
            public const string
            Title = "Title",
            Select = "Select",
            Prefix = "Game",
            Game0 = "Game0",
            Game1 = "Game1";
        }

        public readonly struct Layers
        {
            public const int
            None = 1 << 0,
            Player = 1 << 6,
            Ground = 1 << 7,
            Object = 1 << 8,
            Creature = 1 << 9;
        }

        public readonly struct Keys
        {
            public static string
            Horizontal = "Horizontal",
            ReversedHorizontal = "ReversedHorizontal",
            Vertical = "Vertical",
            Wheel = "Mouse ScrollWheel",
            Jump = "Jump",
            Down = "Down",
            Zoom = "Zoom",
            Button = "Button",
            Pause = "Pause";
        }

        public readonly struct Tags
        {
            public static string
            Manager = "Manager",
            Player = "Player",
            Ladder = "Ladder",
            Pad = "Pad",
            MainCamera = "MainCamera",
            Enemy = "Enemy",
            Panel = "Panel",
            Ice = "Ice",
            Hole = "Hole",
            Mama = "Mama";
        }

        public readonly struct Animations
        {
            public static string
            Idle = "Idle",
            Jump = "Jump",
            Walk = "Walk",
            Venom = "Venom",
            Venomed = "venom_die";
        }

        public readonly struct SpawnPositions
        {
            public static Vector2
            Stage1 = Vector100.Zero;
        }
    }
}
