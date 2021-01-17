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

        Debug.Log(maxOxygen1);   
        // Initialize the oxygenbar
        currentOxygen = maxOxygen1;
        oxygenBar.SetMaxOxygen(maxOxygen1);

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
        Debug.Log(currentOxygen);
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

    public void TestFlowerQuest()
    {
        if (quest.isActive)
        {
            quest.goal.ItemCollected();
            if(quest.goal.IsReached())
            {
                experience += quest.questExperience;
                quest.Complete();
            }
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
        //Debug.Log(lookAngle);
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
        // Lose 1 oxygen every 15 seconds/60 frames
        if (oxy_timer <= 900)
            oxy_timer++;
        else
        {
            LoseOxygen(1);
            oxy_timer = 0;
        }


        // Toggle the inventory/quest window on and off
        if (Input.GetKeyDown("tab"))
        {
            UI_Inventory.SetActive(!UI_Inventory.activeSelf);
            QuestWindow.SetActive(!QuestWindow.activeSelf);
        }

        // TEST for questing system
        if (Input.GetKeyDown("space"))
        {
            TestFlowerQuest();
        }

        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down

        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        // Get the mouse position
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

}
