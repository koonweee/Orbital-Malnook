using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuController : MonoBehaviour
{
    public PlayerMenu player1, player2;

    void Update()
    {
        if (player1.ready && player2.ready)
        {
            player1.ResetPos();
            player2.ResetPos();
        }
    }
}
