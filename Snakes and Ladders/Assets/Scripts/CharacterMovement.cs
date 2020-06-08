using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;

    public float speed = 10f;
    public float startSpeed;

    void Start()
    {
        target = WayPoint.points[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            GetNextWaypoint();
        }

        speed = startSpeed;
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= WayPoint.points.Length - 1)
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = WayPoint.points[wavepointIndex];
    }

    void EndPath()
    {
        Debug.Log("End of Path");
    }

}



