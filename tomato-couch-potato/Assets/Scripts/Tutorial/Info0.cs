using Cysharp.Threading.Tasks;
using UnityEngine;

namespace trrne.Core
{
    public class Info0 : Info
    {
        void Update()
        {
            print("group: " + group == null);
            print("tpanel: " + tpanel == null);
            print("image: " + image == null);
        }

        protected override void OnTriggerEnter2D(Collider2D info)
        {
            if (info.CompareTag(Constant.Tags.Player))
            {
                // await UniTask.WhenAll(StartInfo());
                StartCoroutine(e());
                isDone = true;
            }
        }
    }
}
