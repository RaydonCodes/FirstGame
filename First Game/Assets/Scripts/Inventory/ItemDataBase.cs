using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    private void Awake()
    {
        BuildDataBase();
    }

    public Item GetItem(int id)
    {
        return items.Find(item => item.id == id);
    }

    public Item GetItem(string itemName)
    {
        return items.Find(item => item.title == itemName);
    }


    void BuildDataBase()
    {
        items = new List<Item>(){
                new Item(0,
                Item.ItemType.Throwable,
                "Sharp stone",
                "A stone that hates more than loves.",
                new Dictionary<string, int>
                {
                  {"Damage", 15},
                  {"Range", 10}
                })
            };
    }
}
