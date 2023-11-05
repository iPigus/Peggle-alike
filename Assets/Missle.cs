using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using UnityEngine;

public class Missle : MonoBehaviour
{
    public static List<Missle> list = new();

    public float speed = 10f; // Missile speed
    public float rotationSpeed = 2f; // Rotation speed for homing behavior

    private GameObject ball;

    bool followBall = true;

    private void Awake()
    {
        if (!list.Contains(this)) list.Add(this);
    }

    private void Update()
    {
        if (!ball)
        {
            ball = GameObject.FindGameObjectWithTag("Ball");
        }

        if (ball && followBall)
        {
            if (ball.transform.position.y < -4f)
            {
                followBall = false; return;
            }

            // Rotate towards the ball
            Vector3 direction = (ball.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotationSpeed * Time.deltaTime);

            // Move the missile forward
            transform.position += transform.right * speed * Time.deltaTime;
        }
        else
        {
            // If the ball is not found, continue moving forward
            transform.position += transform.right * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            // Calculate the point of impact
            Vector2 impactPoint = transform.position;

            // Calculate the direction from the impact point to the ball
            Vector2 forceDirection = ((Vector2)other.transform.position - impactPoint).normalized;

            // Apply a force to the ball at the impact point
            Rigidbody2D ballRb = other.GetComponent<Rigidbody2D>();
            ballRb.AddForce(forceDirection * speed, ForceMode2D.Impulse);

            // Destroy the missile
            MusicManager.StartExploseSound();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (list.Contains(this)) list.Remove(this);
    }
}
