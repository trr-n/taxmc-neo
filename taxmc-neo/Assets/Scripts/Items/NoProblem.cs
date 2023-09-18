using System;
using System.Collections;
using System.Collections.Generic;
using trrne.Utils;
using UnityEngine;

namespace trrne.Game
{
    public class NoProblem : Item
    {
        [SerializeField]
        [Range(0, 100)]
        int blankRate = 75;

        protected override void Receive()
        {
            // return;
            // プレイヤーに触れたら
            // blankRate%の確率で初期値に戻す
            if (Gobject.BoxCast2D(out var hit, transform.position, sr.bounds.size, Constant.Layers.Player))
            {
                Lottery.Weighted(new Pair<Action, float>[]
                {
                    // alive
                    new (() => { print("alive"); }, 1),
                    // dead
                    new (() => { print("dead"); }, 1)
                });
            }
        }

        // void OnCollisionEnter2D(Collision2D info)
        // {
        //     if (info.Compare(Constant.Tags.Player))
        //     {
        //         Lottery.Weighted(new Pair<Action, float>[]
        //         {
        //             // alive
        //             new (() => { print("alive"); }, 1),
        //             // dead
        //             new (() => { print("dead"); }, 1)
        //         });

        //     }
        // }
    }
}
