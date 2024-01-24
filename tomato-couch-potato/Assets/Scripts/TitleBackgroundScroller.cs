using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace trrne.Box
{
    public class TitleBackgroundScroller : MonoBehaviour
    {
        [SerializeField]
        GameObject[] wallpapers;

        [SerializeField]
        float scrollSpeed;

        const float RETURN_POINT = -18.96f;

        void Update()
        {
            foreach (var wallpaper in wallpapers)
            {
                wallpaper.transform.Translate(scrollSpeed * Time.unscaledDeltaTime * Vector2.left);
                if (wallpaper.transform.position.x <= RETURN_POINT)
                {
                    wallpaper.transform.SetPosition(x: -RETURN_POINT);
                }
            }
        }
    }
}