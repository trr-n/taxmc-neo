using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class SetBackground : MonoBehaviour
    {
        [SerializeField]
        int count = 10;

        [SerializeField]
        GameObject[] imagePrefabs;

        GameObject[] imageObjs;
        // const float SHIFT = 6.25f;

        void Start()
        {
            imageObjs = new GameObject[transform.childCount];
            imageObjs = transform.GetChildrenGameObject();
            // for (int i = 0; i < imageObjects.Length; ++i)
            // {
            //     // var imageLocalScale = imageObjects[i].transform.localScale;
            //     // var imageLossyScale = imageObjects[i].transform.lossyScale;
            //     // var posx = 0f;
            //     // var posy = imageObjects[i].transform.localPosition.y;
            //     Vector2 pos = new(0, imageObjects[i].transform.localPosition.y);
            //     print(pos.y);
            //     // var imagex = imageObjects[i].GetComponent<SpriteRenderer>().sprite.bounds.size.x;
            //     // var offsetx = posx + imagex;
            //     // print("offsetX: " + offsetx);
            //     for (int j = 0; j < count; ++j)
            //     {
            //         // print(i + ": " + posx);
            //         Instantiate(imagePrefabs[i], pos, Quaternion.identity, transform.GetChild(i));
            //         pos.x += SHIFT; // offsetx;
            //     }
            // }

#if false
            // FIXME 大量入れ子
            for (int i = 0; i < imageObjects.Length; ++i)
            {
                var scaledShiftx = imageObjects[i].GetComponent<SpriteRenderer>().bounds.size.x / transform.localScale.x;
                // print(scaledShiftx);
                // var scalex = imageObjects[i].transform.lossyScale.x;
                for (int j = 0; j < count; ++j)
                {
                    // var x = (SHIFT * scalex * j, 0); 
                    var pos = imageObjects[i].transform.position + new Vector3(scaledShiftx * j, 0); // new Vector3(x, 0);
                    Instantiate(imageObjects[i], pos, Quaternion.identity, transform.GetChild(i));
                    print(transform.GetChild(i).name);
                }
            }
#else
            for (int i = 0; i < imageObjs.Length; ++i)
            {
                var child = transform.GetChild(i);
                var childPos = child.position;
                var scale = child.lossyScale;
                var shiftx = child.GetComponent<SpriteRenderer>().sprite.bounds.size.x * child.transform.lossyScale.x;
                shiftx.Debugs();
                var posx = 0f;
                for (int j = 0; j < count; ++j)
                {
                    var pos = childPos + posx * Vector3.right; // new Vector3(childPos.x * (j + 1), 0);
                    Instantiate(imagePrefabs[i], pos, Quaternion.identity).transform.localScale = scale;
                    posx += shiftx * 0.95f;
                }
            }
#endif
        }
    }
}