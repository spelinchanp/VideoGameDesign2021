using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Item
{
    public enum ItemType
    {
        RedOxygenFlower, 
        OxMeat,
        Iron,
        Null,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
                break;
            case ItemType.RedOxygenFlower:        return ItemAssets.Instance.redOxygenFlowerSprite;
            case ItemType.OxMeat:                 return ItemAssets.Instance.oxMeatSprite;
            case ItemType.Null:                   return ItemAssets.Instance.nullSprite;
        }
        return ItemAssets.Instance.nullSprite;
    }

    public bool isStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.OxMeat:
                return false;
            case ItemType.RedOxygenFlower:
                return true;
            case ItemType.Null:
                return true;
        }
    }
}
