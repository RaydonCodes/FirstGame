using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour
{
    public Item item;
    private Image spriteImage;
    public UIInventory uIInventory;
    public UIItem selectedItem;
    public int index;

    private void Awake()
    {
        spriteImage = GetComponent<Image>();
    }

    public void UpdateItem(Item item)
    {
        this.item = item;
        if(this.item != null)
        {
            spriteImage.color = Color.white;
            spriteImage.sprite = item.icon;
        }
        else
        {
            spriteImage.color = Color.clear;
        }
        if (index < uIInventory.hotbar.hotbarSlotAmount)
        {
            print("toad");
            uIInventory.hotbar.UpdateHotbarSlot(index, item);
        }
    }

    public void OnPointerClickEvent()
    {
        print(index);
        if (item != null)
        {
            if (selectedItem.item != null)
            {
                Item clone = new Item(selectedItem.item);
                selectedItem.UpdateItem(item);
                UpdateItem(clone);
            }
            else
            {
                selectedItem.UpdateItem(item);
                UpdateItem(null);
                
            }
        }
        else if (selectedItem.item != null)
        {
            UpdateItem(selectedItem.item);
            selectedItem.UpdateItem(null);
        }
    }

}
