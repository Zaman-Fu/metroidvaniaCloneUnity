using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    [SerializeField]
    int nextSceneId;
    // Start is called before the first frame update
    public void Continue()
    {
        //TODO: Change this to 2 to proceed to the boss scene. BETTER IDEA: include the boss on the first scene instead. When he's complete.
        SceneManager.LoadScene(nextSceneId);
    }
}
