using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> characterItems = new List<Item>();
    public ItemDataBase itemDataBase;
    public UIInventory inventoryUI;

    private void Start()
    {
      for (int i = 0; i < 10; i++)
        {
            GiveItem(1);
        }
    }

    public void GiveItem(int id)
    {
        Item itemToAdd = itemDataBase.GetItem(id);
        characterItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);
        print("Added item: " + itemToAdd.title);
    }

    public void GiveItem(string itemName)
    {
        Item itemToAdd = itemDataBase.GetItem(itemName);
        characterItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);
        print("Added item: " + itemToAdd.title);
    }


    public Item CheckForItem(int id)
    {
        return characterItems.Find(item => item.id == id);
    }

    public void RemoveItem(int id)
    {
        Item itemToRemove = CheckForItem(id);
        if(itemToRemove != null)
        {
            characterItems.Remove(itemToRemove);
            inventoryUI.RemoveItem(itemToRemove);
            print(itemToRemove.title + " was removed");
        }
    }

}
