using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private int routePosition;
    private bool isMoving;

    public int steps;
    public Route currentRoute;
    public Dice currentDice;

    public bool movePlayer = true;

    public void PlayerMovement()
    {
        if(!isMoving)
        {
            Debug.Log("The function is at steps");
            steps = currentDice.SideValueCheck(steps);
            Debug.Log("Dice Rolled from other script" + steps);


            if (routePosition + steps <= currentRoute.childNodeList.Count)
            {
                Debug.Log("inside - routePosition + steps ");
                if (movePlayer)
                {
                    Debug.Log("inside - moveplayer if");
                    StartCoroutine(Move());
                }
                
            }
            else
            {
                Debug.Log("Rolled Number is too high " + steps);
            }

        }
    }


    IEnumerator Move()
    {
        Debug.Log("Inside - move");
        if(isMoving)
        {
            yield break;
        }
        isMoving = true;

        while(steps > 0)
        {
            Vector3 nextPos = currentRoute.childNodeList[routePosition + 1].position;
            while(MoveToNextNode(nextPos))
            {
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);

            steps--;
            routePosition++;
        }
        //movePlayer = false;
        isMoving = false;
    }

    private bool MoveToNextNode(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 8f * Time.deltaTime));
    }
}
