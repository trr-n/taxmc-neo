using trrne.Box;
using UnityEngine;

namespace trrne
{
    public struct Config
    {
        public readonly struct Scenes
        {
            public const string Title = "Title";
            public const string Select = "Select";
            public const string Prefix = "Game";
            public const string Game0 = "Game0";
            public const string Game1 = "Game1";
        }

        public readonly struct Layers
        {
            public const int Default = 1 << 0;
            public const int Player = 1 << 6;
            public const int Jumpable = 1 << 7;
            public const int Object = 1 << 8;
            public const int Creature = 1 << 9;
        }

        public readonly struct Keys
        {
            public const string Horizontal = "Horizontal";
            public const string MirroredHorizontal = "ReversedHorizontal";
            public const string Vertical = "Vertical";
            public const string Wheel = "Mouse ScrollWheel";
            public const string Jump = "Jump";
            public const string Down = "Down";
            public const string Zoom = "Zoom";
            public const string Button = "Button";
            public const string Pause = "Pause";
            public const string Respawn = "Respawn";
        }

        public readonly struct Tags
        {
            public const string Manager = "Manager";
            public const string Jumpable = "Jumpable";
            public const string Player = "Player";
            public const string Ladder = "Ladder";
            public const string Pad = "Pad";
            public const string MainCamera = "MainCamera";
            public const string Enemy = "Enemy";
            public const string Panel = "Panel";
            public const string Ice = "Ice";
            public const string Hole = "Hole";
            public const string Mama = "Mama";
            public const string Info = "Info";
            public const string Spectre = "Spectre";
        }

        public readonly struct Animations
        {
            public const string Idle = "Idle";
            public const string Jump = "Jump";
            public const string Walk = "Walk";
            public const string Venom = "Venom";
            public const string Venomed = "venom_die";
            public const string Die = "Die";
        }

        public readonly struct SpawnPositions
        {
            public static Vector2 Stage1 => Coordinate.V30;
        }
    }
}
