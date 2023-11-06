using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float laserSpeed = 10f;
    public float detectionWidth = 0.2f;
    private LineRenderer lineRenderer;
    private bool isShooting = true;
    private float maxDistance = 100f; // Maximum distance the laser can travel

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.startWidth = detectionWidth;
        lineRenderer.endWidth = detectionWidth;
    }

    private void Update()
    {
        if (isShooting)
        {
            maxDistance = 100f;

            Vector3 direction = transform.up; // Use the local up direction
            RaycastHit2D hit = Physics2D.Raycast(transform.position + (.5f * direction.normalized), direction, maxDistance);

            if (hit.collider != null)
            {
                // Hit an object
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position + direction * hit.distance + (hit.distance == 0 ? new() : (.5f * direction.normalized)));

                if (hit.collider.CompareTag("Box"))
                {
                    if (hit.collider.TryGetComponent(out Box box))
                    {
                        box.Hit();
                    }
                }
            }
            else
            {
                // No collision, draw the laser up to the maximum distance
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position + direction * maxDistance);
            }
        }
    }
}
