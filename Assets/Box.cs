using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Box : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) return;

        DestroyPeg();
    }

    async void DestroyPeg()
    {
        spriteRenderer.color = new Color32(255, 255, 255, 100);

        await Task.Delay(3000);

        GameManager.AddPoints(100);
        GameManager.SpawnPointText(100, transform.position);

        this.gameObject.SetActive(false);
    }
}
