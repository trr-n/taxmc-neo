namespace trrne.Core
{
    public interface IGimmick
    {
        void On();
        void Off();
    }

    public abstract class Gimmick : Object, IGimmick
    {
        public abstract void Off();
        public abstract void On();
    }
}