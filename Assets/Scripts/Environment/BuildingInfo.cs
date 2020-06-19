using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInfo : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Player")
            {
                Show();
                return;
            } 
        }

        Hide();
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
