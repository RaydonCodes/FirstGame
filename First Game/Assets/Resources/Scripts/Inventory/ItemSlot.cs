using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
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
    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = Color.grey;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;
    }
}
