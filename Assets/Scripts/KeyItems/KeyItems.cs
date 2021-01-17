using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItems : MonoBehaviour
{
    public PlayerManager playerManager;

    public int selectedKeyItem = 0;

    private void Start()
    {
        SelectKeyItem();
    }

    private void Update()
    {
        int previousSelectedKeyItem = selectedKeyItem;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedKeyItem >= transform.childCount - 1)
                selectedKeyItem = 0;
            else
                selectedKeyItem++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedKeyItem <= 0)
                selectedKeyItem = transform.childCount - 1;
            else
                selectedKeyItem--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedKeyItem = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedKeyItem = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedKeyItem = 2;
        }

        if (previousSelectedKeyItem != selectedKeyItem)
        {
            SelectKeyItem();
            
            if (selectedKeyItem == 0)
                playerManager.keyItem = KeyItem.MiningTool;
            else if (selectedKeyItem == 1)
                playerManager.keyItem = KeyItem.Gun;
            else if (selectedKeyItem == 2)
                playerManager.keyItem = KeyItem.OxygenProcessor;
        }
    }

    void SelectKeyItem()
    {
        int i = 0;
        foreach (Transform keyItem in transform)
        {
            if (i == selectedKeyItem)
            {
                keyItem.gameObject.SetActive(true);
            }
            else
            {
                keyItem.gameObject.SetActive(false);
            }
            i++;
        }
    }
}

public enum KeyItem
{
    Gun,
    MiningTool,
    OxygenProcessor,
}
