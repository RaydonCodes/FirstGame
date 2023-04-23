using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> characterItems = new List<Item>();
    public ItemDataBase itemDataBase;

    private void Start()
    {
        GiveItem(0);
        RemoveItem(0);
    }

    public void GiveItem(int id)
    {
        Item itemToAdd = itemDataBase.GetItem(id);
        characterItems.Add(itemToAdd);
        print("Added item: " + itemToAdd.title);
    }

    public void GiveItem(string itemName)
    {
        Item itemToAdd = itemDataBase.GetItem(itemName);
        characterItems.Add(itemToAdd);
        print("Added item: " + itemToAdd.title);
    }


    public Item CheckForItem(int id)
    {
        return characterItems.Find(item => item.id == id);
    }

    public void RemoveItem(int id)
    {
        Item item = CheckForItem(id);
        if(item != null)
        {
            characterItems.Remove(item);
            print(item.title + " was removed");
        }
    }

}
