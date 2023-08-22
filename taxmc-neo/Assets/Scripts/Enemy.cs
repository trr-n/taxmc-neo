using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Self.Game
{
    public abstract class Enemy : MonoBehaviour
    {
        protected abstract void Move();
        protected abstract void DetectPlayer();
    }
}
