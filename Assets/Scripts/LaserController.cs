using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaserController : MonoBehaviour
{
    List<Transform> childs = new List<Transform>();

    private void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex != 7) Destroy(gameObject);

        for (int i = 0; i < transform.childCount; i++)
        {
            childs.Add(transform.GetChild(i));
        }
    }
    private void Update()
    {
        childs.ForEach(x => x.gameObject.SetActive(Input.GetKey(KeyCode.Space)));
        //childs.ForEach(x => x.gameObject.SetActive(true));
    }
}
