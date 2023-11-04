using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float DegreeConstraintAngle = 120f;

    // Update is called once per frame
    void Update()
    {
        Vector3 worldposMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 relativPos = worldposMouse - transform.position;

        float rot_z = (Mathf.Atan2(relativPos.y, relativPos.x) * Mathf.Rad2Deg) + 90;
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Abs(rot_z) > DegreeConstraintAngle / 2 ? (relativPos.x > 0f ? DegreeConstraintAngle / 2 : -DegreeConstraintAngle / 2) : rot_z);
    }
}
