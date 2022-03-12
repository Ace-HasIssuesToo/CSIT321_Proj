using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Inventory inventory;
    public GameObject itemButton;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))     //when colliding with player
        {
            for (int i = 0; i < inventory.slots.Length; i++)        //for each inventory slot
            {
                if (inventory.isFull[i] == false)   //if inventory slot is not full
                {
                    //item can be added to inventory
                    inventory.isFull[i] = true;     //set inventory slot to full

                    //instantiate item button at first inventory slot that isnt full
                    Instantiate(itemButton, inventory.slots[i].transform, false);       //false as we dont want button in world coordinates

                    //Destroy the item from the screen as user has picked it up
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
