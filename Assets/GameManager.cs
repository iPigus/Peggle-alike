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
    static List<string> WinMessages => new() { "You did it! Victory!", "Impressive win!", "Perfect! You won!" };
    static List<string> LossMessages => new() { "Tough luck this time", "Almost there! Try again", "Keep going, you'll conquer it!" };

    int levelPoints = 0;
    int totalPoints = 0;

    static bool areAllBoxesDestroyed => !Box.AllPegs.Where(x => x).Where(x => x.gameObject.activeSelf).Any();

    int ballCount = 5;
    [SerializeField] GameObject _PointsText; 
    
    private void Awake()
    {
        Singleton = this;        
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

        if (areAllBoxesDestroyed)
        {
            LockGame();
            DisplayGameText(WinMessages[Random.Range(0, WinMessages.Count)]);

            await Task.Delay(2000); if (!Singleton) return;

            SceneSwitcherKeys.Singleton.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); return;
        }

        if (Singleton.ballCount == 0)
        {
            LockGame();
            DisplayGameText(LossMessages[Random.Range(0, LossMessages.Count)]);

            await Task.Delay(2000); if (!Singleton) return;

            SceneSwitcherKeys.Singleton.LoadScene(SceneManager.GetActiveScene().buildIndex); return;
        }
    }
    public static void AddPoints(int points)
    {
        Singleton.totalPoints += points;
        Singleton.levelPoints += points;
        Singleton.UpdatePoints();
    }

    void UpdatePoints()
    {
        Points.UpdatePoints(levelPoints, totalPoints);
    }

    static void DisplayGameText(string text)
    {
        MessageText.Display(text);    
    }

    static void LockGame()
    {
        if (!Spawner.Singleton) return;

        Spawner.Singleton.enabled = false;
    }
}
