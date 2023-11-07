using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace trrne.Box
{
    public class XInput
    {
        public enum Device
        {
            Both,
            Keyboard,
            Pad
        }

        readonly Device device;
        Gamepad pad;
        Keyboard kb;

        // both -------------
        // pad
        (Vector2 left, ButtonControl lpush, Vector2 right, ButtonControl rpush) stick;
        (ButtonControl l, ButtonControl zl, ButtonControl r, ButtonControl zr) trigger;
        (ButtonControl a, ButtonControl b, ButtonControl x, ButtonControl y) buttons;

        // keyboard
        (KeyControl w, KeyControl a, KeyControl s, KeyControl d) wasd;
        (KeyControl up, KeyControl down, KeyControl left, KeyControl right) arrow;
        // -------------------

        public XInput() : this(Device.Both) { }

        public XInput(Device device)
        {
            this.device = device;
            stick = (new(), new(), new(), new());
            trigger = (new(), new(), new(), new());
            buttons = (new(), new(), new(), new());
            wasd = (new(), new(), new(), new());
            arrow = (new(), new(), new(), new());
        }

        public bool Connected => device switch
        {
            Device.Both => pad != null && kb != null,
            Device.Keyboard => kb != null,
            Device.Pad => pad != null,
            _ => throw null
        };

        public void Update()
        {
            switch (device)
            {
                case Device.Both:
                    PadLoad();
                    KeyboardLoad();
                    break;
                case Device.Pad:
                    PadLoad();
                    break;
                case Device.Keyboard:
                    KeyboardLoad();
                    break;
                default:
                    throw null;
            }
        }

        void PadLoad()
        {
            pad = Gamepad.current;
            buttons = (pad.aButton, pad.bButton, pad.xButton, pad.yButton);
            stick = (pad.leftStick.ReadValue(), pad.leftStickButton, pad.rightStick.ReadValue(), pad.rightStickButton);
        }

        void KeyboardLoad()
        {
            kb = Keyboard.current;
            wasd = (kb.wKey, kb.aKey, kb.sKey, kb.dKey);
            arrow = (kb.upArrowKey, kb.downArrowKey, kb.leftArrowKey, kb.rightArrowKey);
        }

        public Vector2 Stick(int index)
        => index switch
        {
            0 => pad.leftStick.ReadValue(),
            1 => pad.rightStick.ReadValue(),
            _ => Vector2.zero
        };
    }
}