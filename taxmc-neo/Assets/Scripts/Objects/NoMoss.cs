using System.Collections;
using System.Collections.Generic;
using trrne.utils;
using UnityEngine;

namespace trrne.Game
{
    // a rolling stone gathers no moss
    public class NoMoss : Objectt
    {
        public enum Spin { Left, Right }
        [SerializeField]
        Spin dir = Spin.Left;

        /// <summary>
        /// 回転速度
        /// </summary>
        readonly float spinSpeed = 15f;

        protected override void Behaviour()
        {
            transform.Rotate((dir == Spin.Left ? Coordinate.z : -Coordinate.z) * spinSpeed * Time.deltaTime, Space.World);
        }
    }
}
