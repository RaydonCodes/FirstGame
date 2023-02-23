using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public Animator animator;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void OpenTrashCan()
    {
        animator.SetBool("IsOpened", true);
    }

}
