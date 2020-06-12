using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
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

    public Stone AI;

    public int LowerRangeValue = 1500;
    public int HigherRangeValue = 2000;


    public float sideValueTime;
    public float resetTime = 5f;
    public float downwardForce = 5f;

    public GameObject PlayerAI;

    public float waitTime = 5f;

    public bool flag = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPos = transform.position;
        rb.useGravity = false;

    }

    public void DiceRoll()
    {
        Debug.Log("Interactable");
        //if (rollDice.interactable)//issue
        //{
            Debug.Log("STart DIce tHrow");
            StartCoroutine("DiceThrow");
        //}
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
    }

    private IEnumerator DiceThrowAI()
    {
        Debug.Log("RollDice Called for AI");
        RollDice();
        yield return new WaitForSeconds(sideValueTime);
        rollDice.gameObject.SetActive(false);
        SideValueCheck(diceValue);
        AI.movePlayer = true;
        AI.PlayerMovement();
        StartCoroutine("Reset");
    }
    
    private void RollDice()
    {
        if (!thrown && !hasLanded)
        {
            Debug.Log("!thrown && !hasLanded if condition");
            thrown = true;
            rb.useGravity = true;            
            rb.AddTorque(Random.Range(1000, 1500), Random.Range(1200, 1500), Random.Range(1000, 1200));
            rb.velocity = new Vector3(00, -downwardForce, 0);
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
        Debug.Log("RollAgain Reset Called");
        thrown = true;
        rb.useGravity = true;
        rb.AddTorque(Random.Range(LowerRangeValue, HigherRangeValue), Random.Range(LowerRangeValue, HigherRangeValue), Random.Range(LowerRangeValue, HigherRangeValue));
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

        Debug.Log("Calling flag");
        if (flag == true)
        {
            Debug.Log("Calling Flag, for true");
            rollDice.interactable = false; 
            yield return new WaitForSeconds(waitTime);
            MoveAI();
            Debug.Log("Flag Changed, now False");
            flag = false;
        }      
      
        else if (flag == false)
        {
            Debug.Log("Else If for flag change");
            //DiceRoll();
            flag = true;
            rollDice.interactable = true;
        }

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

    private void MoveAI()
    {
        //yield return new WaitForSeconds(10f);
        Debug.Log("blah!!");
        StartCoroutine("DiceThrowAI");
        //rollDice.interactable = false;
    }
}
