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

        if (rb.IsSleeping() && !hasLanded && thrown)
        {
            Debug.Log("SideValueCheck - if");
            hasLanded = true;
            rb.useGravity = false;
            rb.isKinematic = true;
            thrown = false;
            SideValueCheck(diceValue);
            if (rb.IsSleeping() && hasLanded && !thrown && diceValue == 0)
            {
                Debug.Log("Roll Again Called");
                RollAgain();
            }
        }
        else if (rb.IsSleeping() && hasLanded && !thrown && diceValue == 0)
        {
            Debug.Log("Roll Again Called");
            RollAgain();
        }
    }

    private void RollDice()
    {
        Debug.Log("Inside Roll Dice");
        if(!thrown && !hasLanded)
        {
            Debug.Log("Inside Roll Dice - if");
            thrown = true;
            rb.useGravity = true;
            rb.AddTorque(Random.Range(500, 1000), Random.Range(500, 1000), Random.Range(500, 1000));
        }
        else // if(thrown && hasLanded)
        {
            Debug.Log("Inside Roll Dice - else");
            Reset();
        }

    }

    private void RollAgain()
    {
        Debug.Log("Inside Roll Again Reset Called");
        Reset();
        thrown = true;
        rb.useGravity = true;
        rb.AddTorque(Random.Range(500, 1000), Random.Range(500, 1000), Random.Range(500, 1000));
    }

    private void Reset()
    {
        Debug.Log("Inside Reset");
        transform.position = initialPos;
        thrown = false;
        hasLanded = false;
        rb.useGravity = false;
        rb.isKinematic = false;
        
    }

    public int SideValueCheck(int value)
    {
        Debug.Log("SideValueCheck - inside");
        foreach(DiceSide side in diceSides)
        {
            if(side.OnGround())
            {
                value = side.sideValue;
                Debug.Log(value + " has been rolled!");                
            }
        }
        return value;
    }

}
