using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class PC : Object
    {
        readonly Color[] colors = { Color.red, Color.green };

        SpriteRenderer display;

        bool isLoading = false;

        protected override void Start()
        {
            base.Start();

            display = transform.GetChild(0).GetComponent<SpriteRenderer>();
            // TODO ディスプレイの色じゃなくてチェックマークみたいなんがでるアニメーションに差し替え、余裕があれば
            display.color = colors[0];
        }

        protected override void Behavior() { }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (!isLoading && info.TryGet(out Player player))
            {
                isLoading = true;

                effects.TryGenerate(transform.position);

                display.color = colors[1];
                player.SetCheckpoint(new(transform.position.x, Maths.Cutail(transform.position.y)));
            }
        }
    }
}
