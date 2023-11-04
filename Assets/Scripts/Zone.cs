using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] int Points;

    new Collider2D collider;
    TextMeshProUGUI text;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        text.text = Points.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Ball")) return;

        if (ballClearing != null) StopCoroutine(ballClearing);
        ballClearing = StartCoroutine(ClearBall(collision.gameObject));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Ball")) return;

        if (ballClearing != null) StopCoroutine(ballClearing);
    }

    Coroutine ballClearing = null;

    IEnumerator ClearBall(GameObject ball, float time = 3f)
    {
        yield return new WaitForSeconds(time);

        GameManager.ClearedBall(Points);
        GameManager.SpawnPointText(Points, ball.transform.position);
        Destroy(ball); 
    }
}
