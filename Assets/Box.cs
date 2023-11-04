using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] bool isStrong = false;
    public static List<Box> AllPegs = new();

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        AllPegs.Add(this);

        if (isStrong && spriteRenderer) spriteRenderer.color = new Color32(244, 255, 0, 255); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) return;

        if (isStrong)
        {
            if (transform.childCount != 0) transform.GetChild(0).gameObject.SetActive(true);
            isStrong = false;

            return;
        }

        DestroyPeg();
    }

    async void DestroyPeg()
    {
        spriteRenderer.color = new(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 100f/255f);

        await Task.Delay(3000);

        GameManager.AddPoints(100);
        GameManager.SpawnPointText(100, transform.position);

        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (AllPegs.Contains(this)) AllPegs.Remove(this);
    }
}
