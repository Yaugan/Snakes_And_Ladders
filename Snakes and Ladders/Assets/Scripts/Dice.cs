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

    public Stone player;


    public float sideValueTime;
    public float resetTime;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPos = transform.position;
        rb.useGravity = false;

    }

    public void DiceRoll()
    {
        if(rollDice.interactable == true)
        {
            StartCoroutine("DiceThrow");
        }
        
    }

    private IEnumerator DiceThrow()
    {
        Debug.Log("RollDice Called");
        RollDice();
        yield return new WaitForSeconds(sideValueTime);
        rollDice.gameObject.SetActive(false);
        SideValueCheck(diceValue);
        player.movePlayer = true;
        player.PlayerMovement();
        StartCoroutine("Reset");

        //if (rb.IsSleeping() && !hasLanded && thrown)
        //{
        //    Debug.Log("Side Value is Checked");
        //    hasLanded = true;
        //    rb.useGravity = false;
        //    rb.isKinematic = true;

        //    Debug.Log("This is runned");
        //    SideValueCheck(diceValue);
        //}
        //else if (rb.IsSleeping() && hasLanded && !thrown)
        //{
        //    Debug.Log("Roll Again called");
        //    RollAgain();
        //}
    }

    private void RollDice()
    {

        if (!thrown && !hasLanded)
        {
            Debug.Log("!thrown && !hasLanded if condition");
            thrown = true;
            rb.useGravity = true;            
            rb.AddTorque(Random.Range(500, 1000), Random.Range(500, 1000), Random.Range(500, 1000));
            //SideValueCheck(diceValue);
            //player.movePlayer = true;
            //player.PlayerMovement();
            //StartCoroutine("Reset");

        }
        else if (thrown && hasLanded)
        {
            Debug.Log("Reset Called");
            StartCoroutine("Reset");
        }

    }

    private void RollAgain()
    {
        Debug.Log("RollAgain Accessed");
        //Reset();
        Debug.Log("RollAgain Reset Called");
        thrown = true;
        rb.useGravity = true;
        rb.AddTorque(Random.Range(500, 1000), Random.Range(500, 1000), Random.Range(500, 1000));
    }

    public IEnumerator Reset()
    {
        yield return new WaitForSeconds(resetTime);

        Debug.Log("Reset Accessed");
        transform.position = initialPos;
        thrown = false;
        hasLanded = false;
        rb.useGravity = false;
        rb.isKinematic = false;

        yield return new WaitForSeconds(.5f);
        rollDice.gameObject.SetActive(true);
        //RollAgain();
    }



    internal int SideValueCheck(int value)
    {
        
        Debug.Log("Side Value Checked" + value);
        foreach (DiceSide side in diceSides)
        {
            if (side.OnGround())
            {
                value = side.sideValue;
                Debug.Log(value + " has been rolled!");
            }
        }
        Debug.Log("value: " + value);
       
        return value;
    }



}
