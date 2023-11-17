using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1f)]
    float value;

    Scrollbar bar;

    void Start()
    {
        bar = GetComponent<Scrollbar>();
    }

    void Update()
    {
        bar.value = value;
    }
}
