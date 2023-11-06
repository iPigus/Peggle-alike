using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    private void Start()
    {
        GameManager.totalPoints = 0;
    }
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
