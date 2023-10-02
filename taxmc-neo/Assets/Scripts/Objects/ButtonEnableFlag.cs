using System.Collections;
using System.Collections.Generic;
using trrne.Bag;
using UnityEngine;

namespace trrne.Body
{
    public class ButtonEnableFlag : MonoBehaviour
    {
        bool hit;
        public bool Hit => hit;

        void OnTriggerEnter2D(Collider2D info)
        {
            if (info.Compare(Constant.Layers.Player))
            {
                hit = true;
            }
        }

        void OnTriggerExit2D(Collider2D info)
        {
            if (info.Compare(Constant.Layers.Player))
            {
                hit = false;
            }
        }
    }
}