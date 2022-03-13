using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;

    //for collision trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var item = collision.GetComponent<Item>();
        if (item)   //if it was able to find an item
        {
            inventory.AddItem(item.item, 1);    //add 1 of the item to inventory
            Destroy(collision.gameObject);      //destroy the picked up item
        }
    }

    //clear items in inventory
    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}
