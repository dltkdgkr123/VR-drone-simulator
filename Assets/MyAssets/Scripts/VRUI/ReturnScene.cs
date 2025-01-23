using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnScene : MonoBehaviour
{
    float t = 0f;
    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if(t >= 10f)
        {
            SceneManager.LoadScene("TestMainMenuScene");
        }
    }
}
