using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void credit()
    {
        SceneManager.LoadScene("Credit");
    }
    public void mainmenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void start()
    {
        SceneManager.LoadScene("desert");
    }
    public void tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void restart()
    {
        SceneManager.LoadScene("desert");
    }
    public void exit()
    {
        Application.Quit();
    }
}
