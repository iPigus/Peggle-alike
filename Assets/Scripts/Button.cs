using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] List<GameObject> OnTouchDisable = new();
    [SerializeField] Sprite DisabledGraphic;

    SpriteRenderer spriteRenderer;
    new Collider2D collider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) return;

        spriteRenderer.color = new(1, 1, 1, 100 / 255);
        spriteRenderer.sprite = DisabledGraphic;
        OnTouchDisable.ForEach(x => x.gameObject.SetActive(false));
        collider.enabled = false;
    }
}
