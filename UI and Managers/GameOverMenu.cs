using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    Text resultText;
    public void WinText(bool didWin)
    {
        if(didWin)
        {
            resultText.text = "WIN!";
            resultText.color = Color.green;
        }
    }    
    public void RestartStage()
    {
        Debug.Log("REstarting");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void QuitToMain()
    {
        Debug.Log("Backtomain");
        SceneManager.LoadScene(0);
    }
}
