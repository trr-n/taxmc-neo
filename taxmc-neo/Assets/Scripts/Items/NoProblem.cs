using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using trrne.Utils;

namespace trrne.Game
{
    public class NoProblem : Item
    {
        [SerializeField]
        [Tooltip("base: 1")]
        float blankRate = 1.2f;

        [SerializeField]
        GameObject delFx;

        protected override async void Receive()
        {
            // プレイヤーに触れたらblankRate%の確率で初期値に戻す
            if (Gobject.BoxCast2D(out var hit, transform.position, sr.bounds.size, Constant.Layers.Player))
            {
                // Lottery.Weighted(new Pair<Action, float>[]
                // {
                //     // はずれ
                //     new (() => Runner.NothingSpecial(), 1),

                //     // あたり
                //     new (() => { hit.Get<Player>().Die(); }, 1 * blankRate)
                // });

                switch (Lottery.Weighted(1, 1 * blankRate))
                {
                    case 0:
                        Runner.NothingSpecial();
                        break;

                    case 1:
                    default:
                        await hit.Get<Player>().Die();
                        break;
                }

                // delFx.Generate(transform.position);
                Destroy(gameObject);
            }
        }
    }
}
