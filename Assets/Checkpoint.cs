using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static List<Checkpoint> Checkpoints = new();

    bool isTapped = false;
    bool bonusGranted = false;

    [SerializeField] int number;
    TextMeshProUGUI text;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (!Checkpoints.Contains(this)) Checkpoints.Add(this);
        text = GetComponentInChildren<TextMeshProUGUI>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (text) text.text = number.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) return;

        if (bonusGranted) return;

        MusicManager.PlayCheckpointSound();
        isTapped = !isTapped;

        spriteRenderer.color = new(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, isTapped ? 1f : .5f);

        if (number > 1)
        {
            if (Checkpoints.Where(x => x.number < number && !x.isTapped).Any()) Checkpoints.ForEach(x => { isTapped = false; 
                spriteRenderer.color = new(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, isTapped ? 1f : .5f); });                                        
        }

        if (!Checkpoints.Where(x => !x.isTapped).Any())
        {
            GameManager.DisplayGameText("Checkpoint\nBonus!\n+1000");
            GameManager.AddPoints(1000);
            GameManager.SpawnPointText(1000, transform.position);
            Checkpoints.ForEach(x => x.bonusGranted = true);
            spriteRenderer.color = new(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        }
    }

    private void OnDestroy()
    {
        if (Checkpoints.Contains(this)) Checkpoints.Remove(this);
    }
}
