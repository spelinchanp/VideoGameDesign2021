using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronOre : MonoBehaviour
{
    public PlayerManager player;

    public int maxHealth1 = 100;
    public int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth1;
        Debug.Log("HOLY FUCVKING SHIT OH M GOFNGSEG");
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentHealth <= 0)
        {
            Break();
        }
    }

    private void OnMouseDown()
    {
        if (player.keyItem == KeyItem.MiningTool)
            currentHealth -= 15;
    }
    
    private void Break()
    {
        // drop items
        Item iron = new Item();
        iron.amount = Random.Range(1, 5);
        iron.itemType = Item.ItemType.Iron;
        ItemWorld.SpawnItemWorld(transform.position, iron);

        // destroy gameObject
        Destroy(gameObject);
    }
}
