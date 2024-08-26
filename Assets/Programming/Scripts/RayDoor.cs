using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayDoor : MonoBehaviour
{
    public Animator animator;
    public bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        isOpen = false;
        animator.SetBool("Open", isOpen);

    }

    // Update is called once per frame
    public void Interaction()
    {
        isOpen = !isOpen;
        animator.SetBool("Open", isOpen);
    }
}
