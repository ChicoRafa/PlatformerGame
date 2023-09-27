using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void LoadGameLevel(int levelID)
    {
        SceneManager.LoadScene(levelID);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
