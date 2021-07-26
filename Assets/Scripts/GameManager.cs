using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject PanelBtnMenu;
    // public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public Button pauseButton;
    public Animator playerAnim;

    public void PauseBtn()
    {
        AudioManager.instance.BgSoundPause();
        AudioManager.instance.Play("Click");
        // if (GameIsPaused)
        // {
        //     Resume();
        // }
        // else
        // {
        //     Pause();
        // }
        Pause();
    }
    public void Resume()
    {
        AudioManager.instance.Play("Theme");
        AudioManager.instance.Play("Click");
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        pauseButton.interactable =true;
        // GameIsPaused = false;
    }
    private void Pause()
    {
       
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        pauseButton.interactable = false;
        // GameIsPaused = true;
       
    }
    public void RetryGame()
    {
        AudioManager.instance.Play("Click");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AudioManager.instance.Play("Theme");
    }
    public void Home()
    {
        
        AudioManager.instance.Play("Click");
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        AudioManager.instance.Play("Theme");
    }
    public void QuitGame()
    {
        AudioManager.instance.Play("Click");
        Application.Quit();
    }

}
