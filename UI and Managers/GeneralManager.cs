using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton accessible to all.
/// </summary>
public class GeneralManager : MonoBehaviour
{
    static GeneralManager manager;
    [SerializeField]
     GameObject pauseMenu;
    [SerializeField]
    GameObject gameOverMenu;
    [SerializeField]
    GameObject clearStageMenu;
    private void Start()
    {
        //bind it to
        manager = this;
    }
    private void Update()
    {
       if( Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public static void GameOver(bool didWin)
    {
        Time.timeScale = 0;
        manager.gameOverMenu.SetActive(true);
        manager.gameOverMenu.GetComponent<GameOverMenu>().WinText(didWin);
    }

    public static void PauseMenuOpen()
    {
        Time.timeScale = 0;
        manager.pauseMenu.SetActive(true);
    }
    public static void PauseMenuClose()
    {
        Time.timeScale = 1;
        manager.pauseMenu.SetActive(false);
    }

    public static void TogglePauseMenu()
    {
        if(manager.pauseMenu.activeSelf)
        {
            PauseMenuClose();
        }
        else
        {
            PauseMenuOpen();
        }
    }

    public static void WinGame()
    {
        Time.timeScale = 0;
        manager.clearStageMenu.SetActive(true);
    }
}
