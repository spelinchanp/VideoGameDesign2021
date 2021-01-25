using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public GameObject UI_Inventory;
    public PlayerManager player;

    public float bulletForce = 5f;
        

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        // Don't shoot if the inventory is open or gun isn't selected.
        if (Input.GetButtonDown("Fire1") && UI_Inventory.activeSelf != true && player.keyItem == KeyItem.Gun)
        {
            Shoot();
        }
    }
}
