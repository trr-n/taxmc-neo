using System;

namespace Self.Utils
{
    [Serializable]
    public class Karappoyanke : Exception
    {
        public Karappoyanke() : base("からっぽえんぷてぃ") {; }
        public Karappoyanke(string msg = null) : base(msg) {; }
    }

    [Serializable]
    public class Eguitte : Exception
    {
        public Eguitte() : base("えぐいて!") {; }
        public Eguitte(string msg = null) : base("えぐいて!" + msg) {; }
    }
}