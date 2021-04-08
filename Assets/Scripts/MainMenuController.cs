using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
 
public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;

    public void playGame() {
        SceneManager.LoadScene("Intro");
    }
 
    public void options() 
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void back() 
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
 
    public void exitGame() {
        Application.Quit();
    }


}