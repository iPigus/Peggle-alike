using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleSpawner : MonoBehaviour
{
    [SerializeField] GameObject misslePrefab;
    GameObject ball;

    float timeSinceSpawn = 10f;

    private void FixedUpdate()
    {
        if (!ball) ball = GameObject.FindGameObjectWithTag("Ball");

        bool spawnBalls = ball; 

        //Debug.LogError("before " + spawnBalls);
        if (spawnBalls) spawnBalls = ball.transform.position.y > .4f;
        //Debug.LogError("after " + spawnBalls);

        if (spawnBalls && timeSinceSpawn > 1f)
        {
            Instantiate(misslePrefab, transform.position, Quaternion.identity);
            timeSinceSpawn = 0f;
        }

        timeSinceSpawn += Time.fixedDeltaTime;
    }
}
