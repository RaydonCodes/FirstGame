using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hotbar : MonoBehaviour
{
    public GameObject hotbarSlot;
    public int hotbarSlotAmount = 9;
    private List<GameObject> slotList = new List<GameObject>();

    void Start()
    {
        for(int i = 0; i < hotbarSlotAmount; i++)
        {
            GameObject instance = Instantiate(hotbarSlot);
            slotList.Add(instance);
            instance.transform.SetParent(gameObject.transform);
            instance.transform.localScale = Vector3.one;
            instance.GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();

        }
    }

    public void UpdateHotbarSlot(int slotIndex,Item item)
    {
        Image image = slotList[slotIndex].GetComponentsInChildren<Image>()[1];
        if(item != null)
        {
            image.sprite = item.icon;
            image.color = Color.white;
        }
        else
        {
            image.color = Color.clear;
        }
    }
}
