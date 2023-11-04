using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    new Collider2D collider;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }
}
