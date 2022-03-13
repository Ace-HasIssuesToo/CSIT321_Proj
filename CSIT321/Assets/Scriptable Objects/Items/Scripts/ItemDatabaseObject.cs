using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
//Since Unity doesnt serialize dictionaries, ISerializationCallbackReceiver used to create the dictionary
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] Items;  //array of all items that exist within game
    //make dictionary to import item and return id of item
    public Dictionary<ItemObject, int> GetId = new Dictionary<ItemObject, int>();
    public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();

    public void OnAfterDeserialize()
    {
        GetId = new Dictionary<ItemObject, int>();    //clear dictionary so we are not duplicating anything
        GetItem = new Dictionary<int, ItemObject>();    
        for (int i = 0; i < Items.Length; i++)      //loop through all our items
        {
            GetId.Add(Items[i], i);     //add items and id for the item will be i
            GetItem.Add(i, Items[i]);
            //now everytime unity serializes the scriptable object, it will automatically populate the Dictionary with reference values based off
            //our Items array created in unity editor 
        } 
    }

    //not used
    public void OnBeforeSerialize()
    {
    }
}
