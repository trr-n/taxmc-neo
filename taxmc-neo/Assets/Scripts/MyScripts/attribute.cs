using System;

namespace trrne.utils
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public sealed class WarningAttribute : Attribute
    {
        string warningPoint;

        public WarningAttribute(string warningPoint)
        {
            this.warningPoint = warningPoint;
        }
    }
}