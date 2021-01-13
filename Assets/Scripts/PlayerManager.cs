using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Player management
    private Inventory inventory;

    [SerializeField] private UI_Inventory uiInventory;
    public GameObject UI_Inventory;

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    public int maxOxygen1 = 500;
    public int currentOxygen;
    public OxygenBar oxygenBar;
    public int oxy_timer = 0;

    public QuestGiver QuestGiver;

    public Quest quest;


    private void Awake()
    {
        // Initialize the inventory
        inventory = new Inventory(UseItem);
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);

        // Set the inventory as inactive
        UI_Inventory.SetActive(false);

        // Initialize the healthbar
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        Debug.Log(maxOxygen1);   
        // Initialize the oxygenbar
        currentOxygen = maxOxygen1;
        oxygenBar.SetMaxOxygen(maxOxygen1);


        QuestGiver.UpdateQuestWindow();
    }

    private void Start()
    {

    }

    private void Update()
    {
        // Lose 1 oxygen every 15 seconds/60 frames
        if (oxy_timer <= 900)
            oxy_timer++;
        else
        {
            LoseOxygen(1);
            oxy_timer = 0;
        }
    }


    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void LoseOxygen(int oxygen)
    {
        currentOxygen -= oxygen;
        Debug.Log(currentOxygen);
        oxygenBar.SetOxygen(currentOxygen);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            // Touching Item
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.HealthPotion:
                //FlashGreen();
                inventory.RemoveItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
                break;
            case Item.ItemType.ManaPotion:
                //FlashBlue();
                inventory.RemoveItem(new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
                break;
            case Item.ItemType.MedKit:
                //FlashBlue();
                inventory.RemoveItem(new Item { itemType = Item.ItemType.MedKit, amount = 1 });
                break;
            case Item.ItemType.Coin:
                //FlashBlue();
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Coin, amount = 1 });
                break;
            case Item.ItemType.RedOxygenFlower:
                //FlashBlue();
                inventory.RemoveItem(new Item { itemType = Item.ItemType.RedOxygenFlower, amount = 1 });
                break;
        }
    }

    // Player Control
}
