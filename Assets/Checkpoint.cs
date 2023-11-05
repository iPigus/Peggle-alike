using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static List<Checkpoint> Checkpoints = new();

    bool isTapped = false;

    [SerializeField] int number;
    TextMeshProUGUI text;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (!Checkpoints.Contains(this)) Checkpoints.Add(this);
        text = GetComponentInChildren<TextMeshProUGUI>();
        if (text) text.text = number.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) return;

        isTapped = !isTapped;

        spriteRenderer.color = new(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, isTapped ? 1f : .5f);
    }

    private void OnDestroy()
    {
        if (Checkpoints.Contains(this)) Checkpoints.Remove(this);
    }
}
