using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FloatText : MonoBehaviour
{
    const float animationTime = 1.5f;
    RectTransform rectTransform;
    Vector2 spawnPos;

    private async void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if(rectTransform) spawnPos = rectTransform.anchoredPosition; else spawnPos = transform.position;

        await Task.Delay(300);

        float elapsedTime = 0; 

        while (elapsedTime < animationTime)
        {
            if (rectTransform) rectTransform.anchoredPosition = spawnPos + ((elapsedTime / animationTime) * new Vector2(0, 200f));
            else transform.position = spawnPos + ((elapsedTime / animationTime) * new Vector2(0, 200f));

            await Task.Yield();

            elapsedTime += Time.deltaTime;
        }

        Destroy(transform.root.gameObject);
    }
}
