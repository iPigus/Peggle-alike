using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcherKeys : MonoBehaviour
{
    public static SceneSwitcherKeys Singleton;
    private void Awake()
    {
        Singleton = this;
    }

    int sceneOffset = 1; //we have 1 extra scene (title), for the title, tweak this number as needed
    public void LoadScene(int num)
    {
        if (num < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(num);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (var i = (int) KeyCode.Alpha0; i <= (int) KeyCode.Alpha9; i++)
        {
            if (Input.GetKeyDown((KeyCode)i))
            {
                if (i == (int)KeyCode.Alpha0)
                    i += 10; //hax

                var sceneIndex = i - (int)KeyCode.Alpha1;
                sceneIndex+=sceneOffset;

                LoadScene(sceneIndex);
            }
        }
        for (var i = (int) KeyCode.Keypad0; i <= (int) KeyCode.Keypad9; i++)
        {
            if (Input.GetKeyDown((KeyCode)i))
            {
                if (i == (int)KeyCode.Keypad0)
                    i += 10; //hax

                var sceneIndex = i - (int)KeyCode.Keypad1;
                sceneIndex+=sceneOffset;

                LoadScene(sceneIndex);
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
