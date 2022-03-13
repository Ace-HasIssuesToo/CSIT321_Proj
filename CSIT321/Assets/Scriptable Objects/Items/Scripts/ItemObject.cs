using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Bait,
    Molotov,
    Matchstick,
    Default
}

//base class for creating our items (abstract). We will extend our item objects from this class
public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;   //hold display for item
    public ItemType type;       //store type of item
    
    [TextArea(15,20)]           //so it is not single line string
    public string description;  //hold description of item

}
