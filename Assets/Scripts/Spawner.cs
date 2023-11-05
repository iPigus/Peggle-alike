using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Singleton;

    [SerializeField] GameObject ball;
    [SerializeField] float DegreeConstraintAngle = 120f;
    Transform SpawnPoint;

    private void Awake()
    {
        Singleton = this;
        SpawnPoint = transform.GetChild(2);
    }

    void Update()
    {
        Vector3 worldposMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 relativPos = worldposMouse - transform.position;

        float rot_z = (Mathf.Atan2(relativPos.y, relativPos.x) * Mathf.Rad2Deg) + 90;
        rot_z = Mathf.Abs(rot_z) > DegreeConstraintAngle / 2 ? (relativPos.x > 0f ? DegreeConstraintAngle / 2 : -DegreeConstraintAngle / 2) : rot_z;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);

        if (Input.GetMouseButtonDown(0) && GameManager.canShoot) Shoot(rot_z);
    }

    void Shoot(float rotationZ)
    {
        GameObject spawnedBall = Instantiate(ball, SpawnPoint.transform.position, Quaternion.Euler(0, 0, rotationZ));

        if (spawnedBall.TryGetComponent(out Rigidbody2D rigidbody))
        {
            rigidbody.AddForce(5f * new Vector2(Mathf.Cos((rotationZ - 90f) * Mathf.Deg2Rad), Mathf.Sin((rotationZ - 90f) * Mathf.Deg2Rad)), ForceMode2D.Impulse);
        }
        else Debug.LogError("Couldn't get spawnedball rigidbody2D!");

        GameManager.Shot();
    }
}
