using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private int wallSpeed;
    [SerializeField] private Transform dest;
    private Vector3 destPos;
    private Vector3 origPos;
    private Vector3 direction;

    void Start()
    {
        destPos = dest.position;
        origPos = wall.transform.position;
        direction = dest.position - wall.transform.position;
        direction.Normalize();
    }

    void Update()
    {
        wall.transform.position += direction * wallSpeed * Time.deltaTime;

        if (Vector3.Distance(wall.transform.position, dest.position) < 0.1f && (direction.x * (wall.transform.position.x - origPos.x) > 0))
        {
            direction *= -1;
        }
        else if (Vector3.Distance(wall.transform.position, origPos) < 0.1f && (direction.x * (wall.transform.position.x - destPos.x) > 0))
        {
            direction *= -1;
        }
    }
}
