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
        HealthPotion,
        ManaPotion,
        Coin,
        MedKit,
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
            case ItemType.HealthPotion: return ItemAssets.Instance.healthPotionSprite;
            case ItemType.ManaPotion:   return ItemAssets.Instance.manaPotionSprite;
            case ItemType.Coin:         return ItemAssets.Instance.coinSprite;
            case ItemType.MedKit:       return ItemAssets.Instance.medkitSprite;
        }
        return ItemAssets.Instance.redOxygenFlowerSprite;
    }

    public bool isStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Coin:
                return true;
            case ItemType.HealthPotion:
                return false;
            case ItemType.ManaPotion:
                return false;
            case ItemType.RedOxygenFlower:
                return true;
            case ItemType.MedKit:
                return false;
        }
    }
}
