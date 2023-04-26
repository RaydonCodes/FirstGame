using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    bool hasInteracted = false;
    bool canInteract = false;

    [Header("Interact Button")]
    public float verticalOffset;
    GameObject InteractButton;
    

    private void Start()
    {
        InteractButton = GameObject.FindWithTag("Interaction Button");
        InteractButton.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            hasInteracted = true;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Chest")
        {
            canInteract = false;
            hasInteracted = false;

            InteractButton.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Chest")
        {
            canInteract = true;

            InteractButton.transform.position = other.gameObject.transform.position + new Vector3(0, verticalOffset, 0);
            InteractButton.SetActive(true);
        }

        if (hasInteracted && other.gameObject.tag == "Chest")
        {
            hasInteracted = false;
            ContainerOpener container = other.gameObject.GetComponent<ContainerOpener>();
            if (!container.hasBeenOpened)
            {
                // Open the trashcan
                StartCoroutine(container.OpenChest());
            }
        }
    }
}


