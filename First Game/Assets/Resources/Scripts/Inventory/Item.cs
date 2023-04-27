using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int id;
    public string title;
    public ItemType itemType;
    public string description;
    public Sprite icon;
    public Dictionary<string, int> stats = new Dictionary<string, int>();
    

    public enum ItemType
    {
        Throwable,
        Sword,
        Food
    }

    public Item(int id, ItemType itemType,string title, string description, Dictionary<string, int> stats)
    {
        this.id = id;
        this.itemType = itemType;
        this.title = title;
        this.description = description;
        if (itemType == ItemType.Food){
            this.icon = Resources.Load<Sprite>("Sprites/Items/Food/" + title);
        }
        else
        {
            this.icon = Resources.Load<Sprite>("Sprites/Items/" + title);
        }
        this.stats = stats;
    }

    public Item(Item item)
    {
        this.id = item.id;
        this.title = item.title;
        this.description = item.description;
        if (item.itemType == ItemType.Food)
        {
            this.icon = Resources.Load<Sprite>("Sprites/Items/Food/" + item.title);
        }
        else
        {
            this.icon = Resources.Load<Sprite>("Sprites/Items/" + item.title);
        }
        this.stats = item.stats; 
    }

}
