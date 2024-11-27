using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject GameOverScreen;
    public GameObject WinScreen;
    public AudioSource BMG;
    public AudioSource GameWin;
  
    public void gameOver()
    {
        GameOverScreen.SetActive(true);
    }

    public void Win()
    {
        WinScreen.SetActive(true);
        BMG.Stop();
        GameWin.Play();

    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
