using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    Button contButton;
    [SerializeField]
    Button optButton;
    [SerializeField]
    Button quitButton;

    private void Start()
    {
        contButton.onClick.AddListener(ResumeGame);
        optButton.onClick.AddListener(Options);
        quitButton.onClick.AddListener(QuitGame);
    }

    void ResumeGame()
    {
        GeneralManager.PauseMenuClose();
    }

    void Options()
    {
        Debug.Log("options menu not available");
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
