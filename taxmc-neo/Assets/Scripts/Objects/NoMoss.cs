using System.Collections;
using System.Collections.Generic;
using trrne.Appendix;
using UnityEngine;

namespace trrne.Body
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

        protected override void Behavior()
        {
            // 回転
            transform.Rotate(Time.deltaTime * spinSpeed * (dir == Spin.Left ? Coordinate.z : -Coordinate.z), Space.World);
        }
    }
}
