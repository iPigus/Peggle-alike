using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class MessageText : MonoBehaviour
{
    public static MessageText Singleton;

    TextMeshProUGUI Text;
    RectTransform Rect;

    static Vector2 startPos = new(0f, -1000f);
    static Vector2 endPos = new(0f, 200f);

    private void Awake()
    {
        Singleton = this;

        Rect = GetComponent<RectTransform>();
        Text = GetComponent<TextMeshProUGUI>();
    }

    public static async void Display(string text)
    {
        if (!Singleton)
        {
            Debug.LogError("No Singleton!"); return;
        }
        
        Singleton.Text.text = text;
        Singleton.Text.color = Color.white;

        const float moveInTime = .5f;
        float elapsedTime = 0f;

        while (elapsedTime < moveInTime)
        {
            Singleton.Rect.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsedTime / moveInTime);

            await Task.Yield();

            elapsedTime += Time.deltaTime;
        }

        Singleton.Rect.anchoredPosition = endPos;

        elapsedTime = 0f;

        const float showTime = 1.2f;

        while (elapsedTime < showTime)
        {
            Singleton.Rect.localScale = Vector2.Lerp(new(1, 1), new(1.2f, 1.2f), elapsedTime / showTime);

            await Task.Yield();

            elapsedTime += Time.deltaTime;
        }

        Singleton.Rect.localScale = new(1.2f, 1.2f);

        elapsedTime = 0f;

        const float hideTime = .2f;

        while (elapsedTime < hideTime)
        {
            Singleton.Rect.localScale = Vector2.Lerp(new(1.2f, 1.2f), new(.6f, .6f), elapsedTime / hideTime);
            Singleton.Text.color = Color.Lerp(Color.white, new(0, 0, 0, 0), elapsedTime / hideTime);

            await Task.Yield();

            elapsedTime += Time.deltaTime;
        }

        Singleton.Rect.localScale = new(.6f, .6f);
        Singleton.Text.color = new(0, 0, 0, 0);
        
    }
}
