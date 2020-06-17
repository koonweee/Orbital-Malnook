using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodHeal : MonoBehaviour
{
    public StateController controller;
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Food")
            {
                controller.Heal(1);
            } 
        }
    }
}
