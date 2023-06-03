using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitPointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waitPoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;

    private void Update()
    {
        if (Vector2.Distance(waitPoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            if(currentWaypointIndex >= waitPoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waitPoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }
}
