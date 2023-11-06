using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBox : MonoBehaviour
{
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 endPos;
    [SerializeField] private float moveSpeed;

    private bool isGoingBack = false;
    private float distance;

    private void Start()
    {
        distance = Vector2.Distance(startPos, endPos);
    }

    private void Update()
    {
        float step = moveSpeed * Time.deltaTime;

        if (isGoingBack)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPos, step);
            if (Vector2.Distance(transform.position, startPos) < 0.01f)
            {
                isGoingBack = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, endPos, step);
            if (Vector2.Distance(transform.position, endPos) < 0.01f)
            {
                isGoingBack = true;
            }
        }
    }
}
