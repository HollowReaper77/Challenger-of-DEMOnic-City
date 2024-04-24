using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{

    public int raceNum;

    public void OpenScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Race "+ raceNum.ToString());
        Debug.Log(raceNum);
    }

    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
