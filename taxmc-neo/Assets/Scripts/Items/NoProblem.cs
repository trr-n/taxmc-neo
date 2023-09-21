using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using trrne.utils;

namespace trrne.Game
{
    public class NoProblem : Item
    {
        [SerializeField]
        [Tooltip("base: 1")]
        float blanc = 1.2f;

        [SerializeField]
        GameObject delFx;

        protected override async void Receive()
        {
            // プレイヤーに触れたらblankRate%の確率で初期値に戻す
            if (Gobject.BoxCast2D(out var hit, transform.position, sr.bounds.size, Constant.Layers.Player))
            {
                // Blanc OR Negro
                switch (Lottery.Weighted(1, 1 * blanc))
                {
                    // blanc
                    case 0:
                        Runner.NothingSpecial();
                        break;

                    // negro
                    case 1:
                    default:
                        await hit.Get<Player>().Die();
                        break;
                }

                if (delFx != null)
                {
                    // TODO add fx
                    delFx.Generate(transform.position);
                }
                Destroy(gameObject);
            }
        }
    }
}
