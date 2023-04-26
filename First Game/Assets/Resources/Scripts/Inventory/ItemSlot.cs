using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{

    public UIItem childUIItem;
    private Image image;

    private void Awake()
    {
        image = gameObject.GetComponent<Image>();
        childUIItem = transform.GetComponentInChildren<UIItem>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        childUIItem.OnPointerClickEvent();
    }

    private void OnMouseOver()
    {
        image.color = Color.grey;
        print("s");
    }

    private void OnMouseExit()
    {
        image.color = Color.white;
    }
}
