using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath;     //path that we want to save our file to
    private ItemDatabaseObject database;
    //List of InventorySlot to hold our items
    public List<InventorySlot> Container = new List<InventorySlot>();

    //make sure our database variable is set
    private void OnEnable()
    {
        //make a check to ensure this code is only being run in unity editor
#if UNITY_EDITOR
        //every time scriptable object runs this function, automatically sets database to where the file is inside the editor
        database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDatabaseObject));
#else
        database = Resources.Load<ItemDatabaseObject>("Database");
#endif
    }

    public void AddItem(ItemObject _item, int _amount)
    {
        //find out whether the item we are trying to add in our inventory or not
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == _item)
            {
                //loop true our Container(inventory), if item matching, add to that amount (Stacking) and return.
                Container[i].AddAmount(_amount);
                return;
            }
        }
        //But if no items matching currently in inventory, create a new item
        Container.Add(new InventorySlot(database.GetId[_item], _item, _amount));    //use database's GetID to get ID and populate into InventorySlot
    }

    //functions for saving and loading inventory
    public void Save()
    {
        //use binary formatter in JSON utility
        //use JSON utility to serialize scriptable object out to a string
        string saveData = JsonUtility.ToJson(this, true);   //true for prettyPrint to format output for readability

        //use binary formatter and filestream to create a file and write the stream into file and save to given location
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        //Application.persistentDataPath allows us to save files out to persistent path over different types of devices
        FileStream fileStream = File.Create(string.Concat(Application.persistentDataPath, savePath));
        binaryFormatter.Serialize(fileStream, saveData);
        fileStream.Close();
    }

    public void Load()
    {
        //check if we have a save file to load from
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            //if file exists in save path, open the file
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);

            //once file is open, convert file back to Scriptable Object
            JsonUtility.FromJsonOverwrite(binaryFormatter.Deserialize(fileStream).ToString(), this);    //pass in fileStream and paste to this(our scriptable object)

            //close file
            fileStream.Close();
        }
    }

    public void OnAfterDeserialize()
    {
        /* as soon as unity needs to serialize a changed object, look through each item in container and repopulate item slot to ensure its the same item
          (match by id) */
        for (int i = 0; i < Container.Count; i++)   //for each item in our container
        {
            Container[i].item = database.GetItem[Container[i].id];
        }
    }

    //not used
    public void OnBeforeSerialize()
    {
    }
}

[System.Serializable]       //when adding this class to an object, shows up in editor
public class InventorySlot
{
    public int id;
    public ItemObject item;
    public int amount;

    //constructor
    public InventorySlot(int _id, ItemObject _item, int _amount)
    {
        id = _id;
        item = _item;
        amount = _amount;
    }

    //add amount of object to InventorySlot
    public void AddAmount(int value)
    {
        amount += value;
    }
}
