using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    bool hasInteracted = false;
    bool canInteract = false;
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
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Chest")
        {
            canInteract = true;
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


