using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInfo : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, 2f);
        if (collider != null && collider.gameObject.tag == "Player")
        {
            Show();
        } 
        else {
            Hide();
        }
    }

    public void Show()
    {
        animator.SetTrigger("Show");
    }

    public void Hide()
    {
        animator.SetTrigger("Hide");
    }
}
