using System.Collections;
using System.Collections.Generic;
using trrne.Bag;
using UnityEngine;

namespace trrne.Body
{
    public class Foot : MonoBehaviour
    {
        async void OnTriggerEnter2D(Collider2D info)
        {
            switch (info.GetLayer())
            {
                case Constant.Layers.Player:
                    if (info.Try(out Player player))
                    {
                        await player.Die();
                    }
                    break;

                case Constant.Layers.Creature:
                    if (info.Try(out Enemy enemy))
                    {
                        await enemy.Die();
                    }
                    break;
            }
        }
    }
}
