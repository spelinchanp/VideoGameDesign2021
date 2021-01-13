using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;

public class ItemWorld : MonoBehaviour
{
    private Item item;
    private SpriteRenderer spriteRenderer;
    private TextMeshPro textMeshPro;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        if (item.amount > 1)
        {
            textMeshPro.SetText(item.amount.ToString());
        } else
        {
            textMeshPro.SetText("");
        }
    }

    public static ItemWorld DropItem(Vector3 dropPos, Item item)
    {
        // In the future, change this to drop the item wherever the player
        // is facing

        Vector3 randomDir = UtilTools.GetRandomDir();
        ItemWorld itemWorld = SpawnItemWorld(dropPos + randomDir * 2f, item);
        itemWorld.GetComponent<Rigidbody2D>().AddForce(randomDir * 2f, ForceMode2D.Impulse);
        return itemWorld;
    }

    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

}
