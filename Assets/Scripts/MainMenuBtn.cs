using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class MainMenuBtn : MonoBehaviour
{
    
    public GameObject exitPanel;
    public GameObject SettingMenuPanel;
    public GameObject MainMenu;
    public GameObject CreditSectionPanel;
    public AudioMixer audioMixer;

    public TextMeshProUGUI HighScoreTMP;
    public TextMeshProUGUI coinsTMP;
    public TextMeshProUGUI HeartTMP;




    public void PlayScene(int scene)
    {
        AudioManager.instance.Play("Click");
        SceneManager.LoadScene(scene);
    }
    private void Start()
    {
        

        //Load data for Main menu
        // APTMP.text = "Lv: " + (PlayerPrefs.GetInt("AP")).ToString();
        coinsTMP.text = (LoadInfo()).ToString();
        // Debug.Log("Coins MainMenu:" + LoadInfo());
        
        HeartTMP.text = (LoadHeart()).ToString();
        HighScoreTMP.text = "High Score:" + (Helper.Deserialize<int>(Helper.Decrypt(PlayerPrefs.GetString("HighScore")))).ToString();
    }

    public void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.Escape)) //exit panel
        {
            AudioManager.instance.Play("Menu");
            exitPanel.SetActive(true);
            AudioManager.instance.BgSoundPause();
        }
    }
    public void onUserClickYesNo(int choice) //exit panel
    {
        if (choice == 1)
        {
            Application.Quit();
            AudioManager.instance.Play("Click");
        }
        exitPanel.SetActive(false);
        AudioManager.instance.Play("Click");
        AudioManager.instance.Play("Theme");
    }

    //setting Menu
    public void SettingMenu()
    {

        AudioManager.instance.Play("Click");
        MainMenu.SetActive(false);
        SettingMenuPanel.SetActive(true);
    }
    public void SettingToMainMenu() //back button
    {
        AudioManager.instance.Play("Click");
        MainMenu.SetActive(true);
        SettingMenuPanel.SetActive(false);
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volumeMix", volume);
    }
    int LoadInfo()
    {
        if(PlayerPrefs.HasKey("TotalCoins"))
        {
            return Helper.Deserialize<int>(Helper.Decrypt(PlayerPrefs.GetString("TotalCoins")));
        }
        else{
            return 0;
        }
    }
    int LoadHeart()
    {
        if(PlayerPrefs.HasKey("TotalHeart"))
        {
            return Helper.Deserialize<int>(Helper.Decrypt(PlayerPrefs.GetString("TotalHeart")));
        }
        else{
            return 0;
        }
    }
    public void CreditSection()
    {
        AudioManager.instance.Play("Click");
        AudioManager.instance.BgSoundPause();
        CreditSectionPanel.SetActive(true);
        MainMenu.SetActive(false);
    }
    public void CreditSectionToMain()
    {
        AudioManager.instance.Play("Click");
        CreditSectionPanel.SetActive(false);
        MainMenu.SetActive(true);
        AudioManager.instance.Play("Theme");
    }




}
