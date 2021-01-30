using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringHealthBar : MonoBehaviour
{
    Vector3 localScale;
    private GameObject obj;

    private void Awake()
    {
        obj = transform.parent.gameObject;
        Debug.Log(obj.name);
    }

    private void Start()
    {
        localScale = transform.localScale;
    }

    private void Update()
    {
        int health = 0;
        // this is disgusting but it works so ¯\_(ツ)_/¯
        if (obj.name == "Ox")
            health = obj.GetComponent<Ox>().currentHealth;
        if (obj.name == "IronOre")
            health = obj.GetComponent<IronOre>().currentHealth;



        localScale.x = (float)Convert.ToDouble(health) / 100;
        transform.localScale = localScale;
    }
}
