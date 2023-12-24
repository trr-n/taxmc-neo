using UnityEngine;

namespace trrne
{
    public struct Config
    {
        public readonly struct Scenes
        {
            public const string
            TITLE = "Title",
            SELECT = "Select",
            PREFIX = "Game",
            GAME0 = "Game0",
            GAME1 = "Game1";
        }

        public readonly struct Layers
        {
            public const int
            DEFAULT = 1 << 0,
            PLAYER = 1 << 6,
            JUMPABLE = 1 << 7,
            OBJECT = 1 << 8,
            CREATURE = 1 << 9;
        }

        public readonly struct Keys
        {
            public const string
            HORIZONTAL = "Horizontal",
            MIRRORED_HORIZONTAL = "ReversedHorizontal",
            VERTICAL = "Vertical",
            WHEEL = "Mouse ScrollWheel",
            JUMP = "Jump",
            DOWN = "Down",
            ZOOM = "Zoom",
            BUTTON = "Button",
            PAUSE = "Pause",
            RESPAWN = "Respawn",
            MOUSE_ZOOM = "Mouse ScrollWheel Zoom",
            RESET_ZOOM = "ResetZoom";
        }

        public readonly struct Tags
        {
            public const string
            MANAGER = "Manager",
            JUMPABLE = "Jumpable",
            PLAYER = "Player",
            LADDER = "Ladder",
            JUMP_PAD = "Pad",
            MAIN_CAMERA = "MainCamera",
            ENEMY = "Enemy",
            PANEL = "Panel",
            ICE = "Ice",
            HOLE = "Hole",
            MAMA = "Mama",
            INFO = "Info",
            SPECTRE = "Spectre";
        }

        public readonly struct Animations
        {
            public const string
            Idle = "Idle",
            Jump = "Jump",
            Walk = "Walk",
            Venom = "Venom",
            Venomed = "venom_die",
            Die = "Die";
        }

        public readonly struct SpawnPositions
        {
            public static Vector2 Stage1 => Vector2.zero;
        }
    }
}
