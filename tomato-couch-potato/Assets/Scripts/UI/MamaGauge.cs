using trrne.Heart;
using UnityEditor.Scripting;
using UnityEngine;
using UnityEngine.UI;

namespace trrne.Pancreas
{
    public class MamaGauge : MonoBehaviour
    {
        [SerializeField]
        Sprite mamaface, normal;

        GameObject[] mamaObjs;
        Mama[] mamas;
        Image gauge;

        void Start()
        {
            gauge = GetComponent<Image>();

            var _mamas = Gobject.Finds(Constant.Tags.Mama);
            mamaObjs = new GameObject[_mamas.Length];
            mamas = new Mama[_mamas.Length];
            for (int i = 0; i < mamaObjs.Length; i++)
            {
                mamaObjs[i] = _mamas[i];
                mamas[i] = mamaObjs[i].GetComponent<Mama>();
            }
        }

        void Update()
        {
            gauge.sprite = Maths.Twins(gauge.fillAmount, 1) ? mamaface : normal;
            foreach (var mama in mamas)
            {
                if (Maths.Twins(mama.PepperyRatio, 0))
                {
                    return;
                }
                gauge.fillAmount = mama.PepperyRatio;
            }
        }
    }
}
