using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public UIItem childUIItem;
    private Image image;
    bool mouseHovering;
    bool enableOnce;
    bool disableOnce;
    private float tooltipTimer;
    private float timeToShowTooltip = 0.5f;
    private Vector3 lastMousePosition;
    Tooltip toolTip;

    private void Awake()
    {
        image = gameObject.GetComponent<Image>();
        childUIItem = transform.GetComponentInChildren<UIItem>();
        lastMousePosition = Input.mousePosition;
        toolTip = GameObject.Find("Tooltip").GetComponent<Tooltip>();
    }
    private void Update()
    {
        // Check for activating tooltip
        if (lastMousePosition == Input.mousePosition && mouseHovering)
        {
            tooltipTimer += Time.deltaTime;
        }
        else
        {
            tooltipTimer = 0;
        }

        if(tooltipTimer >= timeToShowTooltip && !enableOnce)
        {
            enableOnce = true;
            disableOnce = false;
            if (childUIItem.item != null)
            {
                toolTip.GenerateTooltip(childUIItem.item);
            }
            
        }
        lastMousePosition = Input.mousePosition;

        // Check for desactivating tooltip
        if(!mouseHovering && !disableOnce)
        {
            enableOnce = false;
            disableOnce = true;
            tooltipTimer = 0;
            toolTip.gameObject.SetActive(false);
            print("niggator");
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        childUIItem.OnPointerClickEvent();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = Color.grey;
        mouseHovering = true;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;
        mouseHovering = false;
    }
}
