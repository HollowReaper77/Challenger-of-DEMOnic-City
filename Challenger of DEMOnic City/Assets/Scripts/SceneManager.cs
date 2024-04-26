using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{

    public int raceNum;

    public void OpenScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Race "+ raceNum);
        Debug.Log("Race "+ raceNum);
    }


    // reload scene egy loadscene-el megoldani?

    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        Debug.Log("BackToMenu");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
