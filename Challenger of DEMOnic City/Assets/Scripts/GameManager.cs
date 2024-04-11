using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    public int raceNum;

    public void OpenScene()
    {
        SceneManager.LoadScene("Race "+ raceNum.ToString());
        Debug.Log(raceNum);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
