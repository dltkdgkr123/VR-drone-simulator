using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(sceneName);
            SceneManager.LoadScene(sceneName);
     
    }
}