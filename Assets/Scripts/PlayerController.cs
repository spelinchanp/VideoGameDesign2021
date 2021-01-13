using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;
    public Camera cam;
    public Animator animator;
    public GameObject firePoint;

    public GameObject UI_Inventory;
    public GameObject QuestWindow;

    // angle of the mouseposition relative to the player
    float lookAngle;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    float lastDirection = 0f;
    public float runSpeed = 20.0f;

    Vector2 mousePos;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
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
        } else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        // Rotate the firePoint
        Rigidbody2D fpRb = firePoint.GetComponent<Rigidbody2D>();
        fpRb.rotation = lookAngle;
    }


}
