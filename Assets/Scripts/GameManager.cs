using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;
    public static bool canShoot => Singleton._canShoot && Singleton.ballCount > 0;
    bool _canShoot = true;

    [SerializeField] string levelName;
    static List<string> WinMessages => new() { "You did it!\nVictory!", "Impressive win!", "Perfect!\nYou won!" };
    static List<string> LossMessages => new() { "Tough luck\nthis time", "Almost there!\nTry again", "Keep going,\nyou'll conquer it!" };

    int levelPoints = 0;
    public static int totalPoints
    {
        get => PlayerPrefs.GetInt("totalPoints");
        set => PlayerPrefs.SetInt("totalPoints", value);
    }

    static bool areAllBoxesDestroyed => !Box.AllPegs.Where(x => x).Where(x => x.gameObject.activeSelf).Any();

    int ballCount = 5;
    [SerializeField] GameObject _PointsText;

    bool isCameraFollow = false;
    
    private void Awake()
    {
        Singleton = this;
    }

    private async void Start()
    {
        DisplayGameText("Level " + SceneManager.GetActiveScene().buildIndex + "\n" + levelName);
        UpdatePoints();

        if (SceneManager.GetActiveScene().buildIndex != 8)
        {
            LockGame();

            await Task.Delay(2000);

            UnlockGame();
        }
        else
        {
            LockGame();
            Camera camera = Camera.main;

            await Task.Delay(2000);

            const float transitionTime = 1f;
            float elapsedTime = 0;

            while (elapsedTime < transitionTime)
            {
                camera.orthographicSize = Mathf.Lerp(15, 5, elapsedTime / transitionTime);

                await Task.Yield();

                elapsedTime += Time.deltaTime;
            }

            isCameraFollow = true;

            camera.orthographicSize = 5;
            UnlockGame();
        }
    }

    GameObject ball;
    private void LateUpdate()
    {
        if (!isCameraFollow) return;
        if (ball == null) ball = GameObject.FindGameObjectWithTag("Ball"); 

        if (ball != null)
        {
            Vector3 desiredPosition = ball.transform.position;

            Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, 5f * Time.deltaTime);
            transform.position = new(smoothPosition.x, smoothPosition.y, -10);
        }
    }

    async void MoveCameraBack()
    {
        isCameraFollow = false;
        LockGame();

        await Task.Delay(300);

        Vector3 startPos = transform.position;
        Vector3 endPos = new(0,0,-10);

        const float ZoomOutTime = .5f;
        float elapsedTime = 0f;

        while (elapsedTime < ZoomOutTime)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / ZoomOutTime);
            Camera.main.orthographicSize = Mathf.Lerp(5, 15, elapsedTime / ZoomOutTime); 

            await Task.Yield();

            elapsedTime += Time.deltaTime;
        }

        transform.position = endPos;
        elapsedTime = 0f;

        await Task.Delay(1000);

        while (elapsedTime < ZoomOutTime)
        {
            Camera.main.orthographicSize = Mathf.Lerp(15, 5, elapsedTime / ZoomOutTime);

            await Task.Yield();

            elapsedTime += Time.deltaTime;
        }

        Camera.main.orthographicSize = 5;
        isCameraFollow = true;
        UnlockGame();
    }

    async void CameraZoomOut()
    {
        isCameraFollow = false;
        LockGame();

        await Task.Delay(300);

        Vector3 startPos = transform.position;
        Vector3 endPos = new(0, 0, -10);

        const float ZoomOutTime = .5f;
        float elapsedTime = 0f;

        while (elapsedTime < ZoomOutTime)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / ZoomOutTime);
            Camera.main.orthographicSize = Mathf.Lerp(5, 15, elapsedTime / ZoomOutTime);

            await Task.Yield();

            elapsedTime += Time.deltaTime;
        }

        transform.position = endPos;
    }

    public static void Shot()
    {
        Singleton._canShoot = false;
        Singleton.ballCount -= 1;

        BallCount.SetBallCount(Singleton.ballCount);
    }

    public static void SpawnPointText(int points, Vector2 positon)
    {
        GameObject text = Instantiate(Singleton._PointsText, positon, Quaternion.identity);
        text.GetComponentInChildren<TextMeshProUGUI>().text = "+" + points.ToString();
    }

    public static async void ClearedBall(int points)
    {
        Singleton._canShoot = true;

        AddPoints(points);

        if(Singleton.isCameraFollow) Singleton.MoveCameraBack();

        if (areAllBoxesDestroyed)
        {
            LockGame();
            DisplayGameText(WinMessages[Random.Range(0, WinMessages.Count)]);

            if (SceneManager.GetActiveScene().buildIndex == 8) Singleton.CameraZoomOut();

            await Task.Delay(2000); if (!Singleton) return;

            if (SceneManager.GetActiveScene().buildIndex == 8)
            {
                SceneSwitcherKeys.Singleton.LoadScene(0); return;
            }
            SceneSwitcherKeys.Singleton.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); return;
        }

        if (Singleton.ballCount == 0)
        {
            LockGame();
            DisplayGameText(LossMessages[Random.Range(0, LossMessages.Count)]);

            if (SceneManager.GetActiveScene().buildIndex == 8) Singleton.CameraZoomOut();

            await Task.Delay(2000); if (!Singleton) return;

            SceneSwitcherKeys.Singleton.LoadScene(SceneManager.GetActiveScene().buildIndex); return;
        }
    }
    public static void AddPoints(int points)
    {
        totalPoints += points;
        Singleton.levelPoints += points;
        Singleton.UpdatePoints();
    }

    void UpdatePoints()
    {
        Points.UpdatePoints(levelPoints, totalPoints);
    }


    public static void DisplayGameText(string text)
    {
        MessageText.Display(text);    
    }

    static void LockGame()
    {
        if (!Spawner.Singleton) return;

        Spawner.Singleton.enabled = false;
    }

    static void UnlockGame()
    {
        if(!Spawner.Singleton) return;

        Spawner.Singleton.enabled = true;
    }
}
