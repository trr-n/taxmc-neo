using UnityEngine;

namespace trrne.Core
{
    public abstract class Gimmick : MonoBehaviour, IGimmick
    {
        public abstract void Off();
        public abstract void On();
    }
}