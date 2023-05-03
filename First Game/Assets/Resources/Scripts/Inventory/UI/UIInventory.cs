using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public List<UIItem> uIItems = new List<UIItem>();
    public GameObject slotPrefab;
    public Transform slotPanel;
    public Transform hotbarPanel;
    public int numberOfSlots = 36;
    public UIItem selectedItem;
    public Hotbar hotbar;


    public void CreateSlots()
    {
        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            if(i < 9)
            {
                instance.transform.SetParent(hotbarPanel, false);
            }
            else
            {
                instance.transform.SetParent(slotPanel, false);
            }
            instance.gameObject.AddComponent<ItemSlot>();
            UIItem uIItem = instance.transform.GetComponentInChildren<UIItem>();
            uIItem.index = i;
            uIItem.uIInventory = gameObject.GetComponent<UIInventory>();
            uIItem.UpdateItem(null);
            uIItem.selectedItem = selectedItem;

            uIItems.Add(instance.GetComponentInChildren<UIItem>());
        }
    }

    public void UpdateSlot(int slot, Item item)
    {
        uIItems[slot].UpdateItem(item);
    }
    public void AddNewItem(Item item)
    {
        UpdateSlot(uIItems.FindIndex(i => i.item == null), item);
    }
    public void RemoveItem(Item item)
    {
        UpdateSlot(uIItems.FindIndex(i => i.item == item), null);
    }
}
