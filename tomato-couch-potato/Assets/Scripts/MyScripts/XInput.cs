using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace trrne.Box
{
    public enum XInputDevice
    {
        Both,
        Keyboard,
        Pad
    }

    public class XInput
    {
        readonly XInputDevice device;
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

        public XInput() : this(XInputDevice.Both) { }

        public XInput(XInputDevice device)
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
            XInputDevice.Both => pad != null && kb != null,
            XInputDevice.Keyboard => kb != null,
            XInputDevice.Pad => pad != null,
            _ => throw null
        };

        public void Update()
        {
            switch (device)
            {
                case XInputDevice.Both:
                    PadLoad();
                    KBLoad();
                    break;
                case XInputDevice.Pad:
                    PadLoad();
                    break;
                case XInputDevice.Keyboard:
                    KBLoad();
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

        void KBLoad()
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