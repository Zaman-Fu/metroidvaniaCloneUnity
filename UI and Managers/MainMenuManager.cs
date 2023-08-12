using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Go to level 1 scene
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }

    /// <summary>
    /// To the debug playground
    /// </summary>
    public void ToPlayground()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
