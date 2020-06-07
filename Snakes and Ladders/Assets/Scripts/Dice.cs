using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    private Rigidbody rb;
    private bool hasLanded;
    private bool thrown;
    private Vector3 initialPos;

    public int diceValue;
    public Button rollDice;

    public DiceSide[] diceSides;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPos = transform.position;
        rb.useGravity = false;
    }

    public void DiceThrow()
    {
        if(rollDice.interactable == true)
        {
            RollDice();
        }

        if(rb.IsSleeping() && !hasLanded && thrown)
        {
            hasLanded = true;
            rb.useGravity = false;
            rb.isKinematic = true;

            SideValueCheck();
        }
        else if(rb.IsSleeping() && hasLanded && diceValue == 0)
        {
            RollAgain();
        }
    }

    private void RollDice()
    {

        if(!thrown && !hasLanded)
        {
            thrown = true;
            rb.useGravity = true;
            rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
        }
        else if(thrown && hasLanded)
        {
            Reset();
        }

    }

    private void RollAgain()
    {
        Reset();
        thrown = true;
        rb.useGravity = true;
        rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
    }

    private void Reset()
    {
        transform.position = initialPos;
        thrown = false;
        hasLanded = false;
        rb.useGravity = false;
        rb.isKinematic = false;
    }

    private void SideValueCheck()
    {
        foreach(DiceSide side in diceSides)
        {
            if(side.OnGround())
            {
                diceValue = side.sideValue;
                Debug.Log(diceValue + " has been rolled!");
            }
        }
    }

}
