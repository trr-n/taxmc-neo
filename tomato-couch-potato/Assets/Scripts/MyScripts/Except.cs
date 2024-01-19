using System;

namespace trrne.Box
{
    [Serializable]
    public class KarappoyankeException : Exception
    {
        public KarappoyankeException() : base("nanka kakankai") {; }
        public KarappoyankeException(string msg = null) : base(msg) {; }
    }
}
