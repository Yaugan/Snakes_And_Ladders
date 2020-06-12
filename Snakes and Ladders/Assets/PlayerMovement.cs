using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Dice dice;

    public GameObject player;
    public GameObject playerAI;

    bool flag = true;

    void Movement()
    {
    hostflag:

        if (player.tag == "Player" && flag == true) 
        {
            dice.DiceRoll();
            flag = false;
            goto hostflag;
        }
        else if (player.tag == "PlayerAI" && flag == false)
        {
            //dice.DiceThrow();
            flag = true;
        }
    }

}
