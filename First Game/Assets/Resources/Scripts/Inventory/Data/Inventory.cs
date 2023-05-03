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
        inventoryUI.CreateSlots();
      for (int i = 0; i < 10; i++)
        {
            GiveItem(1);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryUI.gameObject.SetActive(!inventoryUI.gameObject.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            GiveItem(0);
        }
    }

    public void GiveItem(int id)
    {
        Item itemToAdd = itemDataBase.GetItem(id);
        characterItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);
    }

    public void GiveItem(string itemName)
    {
        Item itemToAdd = itemDataBase.GetItem(itemName);
        characterItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);
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
        }
    }

}
