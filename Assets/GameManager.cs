using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;
    public static bool canShoot => Singleton._canShoot && Singleton.ballCount > 0;
    bool _canShoot = true;

    int levelPoints = 0;
    int totalPoints = 0;

    int ballCount = 5;
    bool isFirstRound = true;
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

    public static void ClearedBall(int points)
    {
        Singleton._canShoot = true;

        AddPoints(points);
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
}
