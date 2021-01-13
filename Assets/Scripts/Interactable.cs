using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// check if in range of interaction
// press button to interact
// execute the interaction

public class Interactable : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isInRange) // if we're in range to interact
        {
            if(Input.GetKeyDown(interactKey)) // and player presses key
            {
                interactAction.Invoke(); // fire event
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("Player now in range");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            Debug.Log("Player out of range");
        }
    }
}
