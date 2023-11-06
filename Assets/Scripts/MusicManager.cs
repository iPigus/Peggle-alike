using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    static MusicManager Singleton;
    AudioSource audio;

    [SerializeField] List<AudioSource> SFX = new();

    private async void Awake()
    {
        if (Singleton)
        {
            Destroy(gameObject); return;
        }
        else Singleton = this;

        audio = GetComponent<AudioSource>(); DontDestroyOnLoad(gameObject);

        const float timeToFadeIn = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < timeToFadeIn)
        {
            audio.volume = Mathf.Lerp(0, .1f, elapsedTime / timeToFadeIn);

            await Task.Yield();

            elapsedTime += Time.deltaTime;
        }
    }

    public static void PlayBoxSound()
    {
        if (Singleton) Singleton.SFX[0].Play();
    }

    public static void PlayCheckpointSound()
    {
        if (Singleton) Singleton.SFX[1].Play();
    }

    public static void StartExploseSound()
    {
        if (Singleton) Singleton.SFX[2].Play();
    }
}
