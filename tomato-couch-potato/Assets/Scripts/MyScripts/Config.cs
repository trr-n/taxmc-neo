using UnityEngine;

namespace trrne
{
    public struct Config
    {
        public readonly struct Scenes
        {
            public const string TITLE = "Title";
            public const string SELECT = "Select";
            public const string PREFIX = "Game";
            public const string GAME0 = "Game0";
            public const string GAME1 = "Game1";
        }

        public readonly struct Layers
        {
            public const int DEFAULT = 1 << 0;
            public const int PLAYER = 1 << 6;
            public const int JUMPABLE = 1 << 7;
            public const int OBJECT = 1 << 8;
            public const int CREATURE = 1 << 9;
        }

        public readonly struct Keys
        {
            public const string HORIZONTAL = "Horizontal";
            public const string MIRRORED_HORIZONTAL = "ReversedHorizontal";
            public const string VERTICAL = "Vertical";
            public const string WHEEL = "Mouse ScrollWheel";
            public const string JUMP = "Jump";
            public const string DOWN = "Down";
            public const string ZOOM = "Zoom";
            public const string BUTTON = "Button";
            public const string PAUSE = "Pause";
            public const string RESPAWN = "Respawn";
            public const string MOUSE_ZOOM = "Mouse ScrollWheel Zoom";
            public const string RESET_ZOOM = "ResetZoom";
        }

        public readonly struct Tags
        {
            public const string MANAGER = "Manager";
            public const string JUMPABLE = "Jumpable";
            public const string PLAYER = "Player";
            public const string LADDER = "Ladder";
            public const string JUMP_PAD = "Pad";
            public const string MAIN_CAMERA = "MainCamera";
            public const string ENEMY = "Enemy";
            public const string PANEL = "Panel";
            public const string ICE = "Ice";
            public const string HOLE = "Hole";
            public const string MAMA = "Mama";
            public const string INFO = "Info";
            public const string SPECTRE = "Spectre";
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
            public static Vector2 Stage1 => Vector2.zero;
        }
    }
}
