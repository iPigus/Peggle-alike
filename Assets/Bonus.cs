using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] int pointBonus = 1000;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Ball")) return;

        GameManager.DisplayGameText("Bonus picked up!\n+" + pointBonus);
        GameManager.AddPoints(pointBonus);
        GameManager.SpawnPointText(pointBonus, transform.position);

        Destroy(gameObject);
    }
}
