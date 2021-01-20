using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public bool isOpen;
    public PlayerManager player;
    //public Animator anim;

    public void OpenChest()
    {
        isOpen = !isOpen;
        Debug.Log("Chest was interacted with...");
        player.unlockedOxygenProc = true;
    }
}
