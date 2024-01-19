using UnityEngine;

namespace trrne.Box
{
    public class FlagConditionalDisableInInspectorAttribute : PropertyAttribute
    {
        public readonly string FlagVariableName;
        public readonly bool TrueThenDisable;
        public readonly bool ConditionalInvisible;

        public FlagConditionalDisableInInspectorAttribute(
            string flagVariableName, bool trueThenDisable = false, bool conditionalInvisible = false)
        {
            FlagVariableName = flagVariableName;
            TrueThenDisable = trueThenDisable;
            ConditionalInvisible = conditionalInvisible;
        }
    }
}