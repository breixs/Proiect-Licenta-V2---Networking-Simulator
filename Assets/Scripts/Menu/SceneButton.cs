using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    public string sceneName;
    public void ChangeScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneName);
    }
}
