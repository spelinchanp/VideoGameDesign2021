using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverHealthBar : MonoBehaviour
{
    Vector3 localScale;
    public Ox ox;

    private void Start()
    {
        localScale = transform.localScale;
    }

    private void Update()
    {
        // this is disgusting but it works so ¯\_(ツ)_/¯
        localScale.x = (float)Convert.ToDouble(ox.currentHealth) / 100;
        transform.localScale = localScale;
    }
}
