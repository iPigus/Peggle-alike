using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallCount : MonoBehaviour
{
    static BallCount Singleton;
    List<Transform> balls = new();

    private void Awake()
    {
        Singleton = this;
        for (int i = 0; i < transform.childCount; i++) 
        {
            balls.Add(transform.GetChild(i));
        }

        balls = balls.OrderBy(x => x.transform.position.x).ToList();
    }

    public static void SetBallCount(int count)
    {
        for (int i = 0; i < 5; i++)
        {
            Singleton.balls[i].gameObject.SetActive(count - i > 0);    
        }
    }
}
