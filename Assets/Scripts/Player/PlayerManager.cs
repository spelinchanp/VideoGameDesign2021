using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Player management
    private Inventory inventory;

    [SerializeField] private UI_Inventory uiInventory;
    public GameObject UI_Inventory;
    public KeyItem keyItem;
    public Item.ItemType lastItemPickedUp;
    public bool unlockedOxygenProc = false;

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    public int maxOxygen1 = 500;
    public int currentOxygen;
    public OxygenBar oxygenBar;
    public int oxy_timer = 0;

    public Quest quest;

    public int experience;


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
 
        // Initialize the oxygenbar
        currentOxygen = maxOxygen1;
        oxygenBar.SetMaxOxygen(maxOxygen1);

        Debug.Log(keyItem.ToString());

    }

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void LoseOxygen(int oxygen)
    {
        currentOxygen -= oxygen;
        oxygenBar.SetOxygen(currentOxygen);
    }

    void GainOxygen(int oxygen)
    {
        if (currentOxygen + oxygen >= 500)
            currentOxygen = 500;
        else 
            currentOxygen += oxygen;
        oxygenBar.SetOxygen(currentOxygen);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Pick up an item
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            // Touching Item
            Item item = itemWorld.GetItem();

            // If its a red flower and player is holding an oxygen processor, process it for oxygen
            if (item.itemType == Item.ItemType.RedOxygenFlower && keyItem == KeyItem.OxygenProcessor)
            {
                GainOxygen(20);
                if (quest.number == 2)
                {
                    quest.goal.ItemCollected();
                    if (quest.goal.currentAmount >= quest.goal.requiredAmount)
                        quest.Complete();
                }
                itemWorld.DestroySelf();
            }
            // If the item is not a red flower, pick it up and add it to the inventory
            else if (item.itemType != Item.ItemType.RedOxygenFlower)
            {
                inventory.AddItem(item);
                lastItemPickedUp = item.itemType;
                itemWorld.DestroySelf();
            }
            return;
        }

        Ox ox = collider.GetComponent<Ox>();
        if (ox != null)
        {
            if (ox.isAngry)
            {
                TakeDamage(20);
            }
        }
    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.OxMeat:
                //FlashGreen();
                inventory.RemoveItem(new Item { itemType = Item.ItemType.OxMeat, amount = 1 });
                break;
            case Item.ItemType.RedOxygenFlower:
                //FlashBlue();
                inventory.RemoveItem(new Item { itemType = Item.ItemType.RedOxygenFlower, amount = 1 });
                break;
        }
    }

    // Player Control
    Rigidbody2D body;
    public Camera cam;
    public Animator animator;
    public GameObject firePoint;
    public GameObject QuestWindow;

    // angle of the mouseposition relative to the player
    float lookAngle;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    public float runSpeed;

    Vector2 mousePos;

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);

        // Look direction
        Vector2 lookDir = mousePos - body.position;
        lookAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        if ((lookAngle < -90) || lookAngle > 100)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        // Rotate the firePoint
        Rigidbody2D fpRb = firePoint.GetComponent<Rigidbody2D>();
        fpRb.rotation = lookAngle;
    }

    private void Update()
    {
        // check for quest updates
        if (quest.number == 0 && unlockedOxygenProc)
            quest.Complete();
        if (quest.number == 1 && keyItem == KeyItem.OxygenProcessor)
            quest.Complete(); /* End of level/day 1 */
        if (quest.number == 3 && keyItem == KeyItem.Gun)
            quest.Complete();

        // Lose 1 oxygen every 15 seconds/60 frames
        if (oxy_timer <= 900)
            oxy_timer++;
        else
        {
            LoseOxygen(5);
            oxy_timer = 0;
        }


        // Toggle the inventory/quest window on and off
        if (Input.GetKeyDown("tab"))
        {
            UI_Inventory.SetActive(!UI_Inventory.activeSelf);
            QuestWindow.SetActive(!QuestWindow.activeSelf);
        }

        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down

        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        // Get the mouse position
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

}
