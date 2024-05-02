using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{

    public int raceNum;
    public GameObject MainMenuUI;

    public void OpenScene()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene("TempScene");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Race "+ raceNum);
        Debug.Log("Race "+ raceNum);
    }


    // reload scene egy loadscene-el megoldani?

    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        
        /*
         GameObject mainmenuUI = GameObject.Find("Canvas");

        
        //GameObject mainMenu = mainmenuUI.transform.Find("MainMenu").gameObject;
        //GameObject accountManagementUI = mainmenuUI.transform.Find("AccountManagementMenu").gameObject;
        //accountManagementUI.SetActive(false);
        //AccountManagementMenu
        //mainMenu.SetActive(true);
        //ActivateUIElement("MainMenuUI");
        //DeactivateUIElement("AccountManagementMenu");
        */
        Debug.Log("BackToMenu");

    }
    /*
    public void ActivateUIElement(string elementName)
    {
        GameObject uiElement = GameObject.Find(elementName);
        if (uiElement != null)
        {
            uiElement.SetActive(true);
        }
        else
        {
            Debug.LogWarning("UI element not found: " + elementName);
        }
    }
    */
    /*
    public void DeactivateUIElement(string elementName)
    {
        GameObject uiElement = GameObject.Find(elementName);
        if (uiElement != null)
        {
            uiElement.SetActive(false);
        }
        else
        {
            Debug.LogWarning("UI element not found: " + elementName);
        }
    }
    */
    /*
    public void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    */
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
