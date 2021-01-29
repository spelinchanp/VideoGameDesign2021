using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ox : MonoBehaviour
{
    public Animator anim;
    public Transform player;
    public float moveSpeed = 1f;

    public int maxHealth = 100;
    public int currentHealth;
    public bool isAngry = false;
    public int moveTimer = 0;

    private Rigidbody2D body;
    private Vector2 movement;


    private void Awake()
    {
        Debug.Log(maxHealth);
        currentHealth = maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isAngry)
        {
            anim.SetFloat("speed", 6);
            moveSpeed = 4f;
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if ((angle < -90) || angle > 100)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

            direction.Normalize();
            movement = direction;
        } 
        else
        {
            // Randomly move
            moveTimer++;
            if (moveTimer > 600)
            {
                float moveOrNot = Random.Range(0f, 1f);
                if (moveOrNot >= .50)
                {
                    anim.SetFloat("speed", 1);
                    movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                    moveTimer = 0;
                } else
                {
                    anim.SetFloat("speed", 0);
                    movement = new Vector2(0, 0);
                    moveTimer = 0;
                }

            }

            
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "bullet")
        {
            isAngry = true;
            takeDamage(5);
        }
    }


    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction)
    {
        body.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    void die()
    {
        // drop meat    
        Item meat = new Item();
        meat.amount = Random.Range(1, 3);
        meat.itemType = Item.ItemType.OxMeat;
        ItemWorld.SpawnItemWorld(transform.position, meat);

        // destroy ox object
        Destroy(gameObject);
    }

    void takeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            die();
    }
}
