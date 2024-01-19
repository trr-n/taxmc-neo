using UnityEngine;

namespace trrne.Box
{
    public class FlagConditionalDisableInInspectorAttribute : PropertyAttribute
    {
        public readonly string FlagVarNameStr;
        public readonly bool TrueThenDisable, ConditionalInvisible;

        public FlagConditionalDisableInInspectorAttribute(string flag, bool ttd = false, bool condition = false)
        {
            FlagVarNameStr = flag;
            TrueThenDisable = ttd;
            ConditionalInvisible = condition;
        }
    }
}

// https://mu-777.hatenablog.com/entry/2022/09/04/113348