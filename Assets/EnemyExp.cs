using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExp : MonoBehaviour
{
    public int exp;
    public StateController controller;

    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<StateController>();
    }
    public void GiveExp()
    {
        controller.GainExp(exp);
    }
}
