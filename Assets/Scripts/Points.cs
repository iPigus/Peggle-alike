using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Points : MonoBehaviour
{
    static Points Singleton;

    TextMeshProUGUI levelPoints;
    TextMeshProUGUI totalPoints;
    private void Awake()
    {
        Singleton = this;

        levelPoints = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        totalPoints = transform.GetChild(1).GetComponent<TextMeshProUGUI>();    
    }

    public static void UpdatePoints(int levelPts, int totalPts)
    {
        Singleton.levelPoints.text = PadIntTo6Digits(levelPts);
        Singleton.totalPoints.text = PadIntTo6Digits(totalPts);
    }

    public static string PadIntTo6Digits(int number)
    {
        return number.ToString("D6");
    }
}
