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
        [Tooltip("base: 1")]
        float blankRate = 1.2f;

        [SerializeField]
        GameObject delFx;

        Vector2 initPos = Vector2.zero;

        protected override void Receive()
        {
            // return;
            // プレイヤーに触れたら
            // blankRate%の確率で初期値に戻す
            if (Gobject.BoxCast2D(out var hit, transform.position, sr.bounds.size, Constant.Layers.Player))
            {
                Lottery.Weighted(new Pair<Action, float>[]
                {
                    // はずれ
                    new (() => Runner.NothingSpecial(), 1),

                    // あたり
                    new (() => hit.SetPosition(initPos), 1 * blankRate)
                });

                delFx.Generate(transform.position);
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
