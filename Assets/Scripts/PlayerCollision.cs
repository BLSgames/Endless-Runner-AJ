using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerCollision : MonoBehaviour
{
    public PlayerController movement;
    private Animator playerAnim;
    public TextMeshProUGUI CoinsText, HeartText, ScoreText;
    public TextMeshProUGUI CoinsTextGameOver, HeartTextGameOver, ScoreGameOver;
    public TextMeshProUGUI HighScoreGOPanel, TotalHeartContinueP;
    private int Coins, Score;
    private Transform playerPos;

    private void Start()
    {
        int TotalHeart = Helper.Deserialize<int>(Helper.Decrypt(PlayerPrefs.GetString("TotalHeart", "Dpg2sFGa+1la3wlTQlu2OUBTc0g+GeByxSRP+ndnbFrQ5PAH07Ia5aIBHe65X7XrOgV5DRCtU3Q=")));
        playerAnim = GetComponent<Animator>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        HeartText.text = "X" + (LoadHeart()).ToString();
    }
    void FixedUpdate()
    {
        Score = (int)playerPos.position.z - 3;
        ScoreText.text = "Score:" + (Score).ToString();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Obstacles")
        {
            StartCoroutine(ContinueToGameOver());
            ObstacleToDestroy = other.gameObject;
        }
    }
    IEnumerator ContinueToGameOver()
    {
        movement.enabled = false;
       
        AudioManager.instance.Play("PlayerDeath"); // playing death sound
        AudioManager.instance.BgSoundPause();
        playerAnim.Play("Death");
         

        yield return new WaitForSeconds(3);

        AudioManager.instance.Play("Theme");

        if (continueCount == 0)
        {
            continueCount = 1;
        }
        else
        {
            continueValue = continueValue * 2;
        }

        if (LoadHeart() >= continueValue)
        {
            TouchPanelMenu.SetActive(false);
            Time.timeScale = 0f;
            ContinuePanel.SetActive(true);

            // SaveHeart();
            TotalHeartContinueP.text = continueValue.ToString() + "/" + (LoadHeart()).ToString();

        }
        else
        {
            TouchPanelMenu.SetActive(false);
            Time.timeScale = 0f;
            GameOverMenuPanel.SetActive(true);

            // for coins
            CoinsTextGameOver.text = "+" + (Coins).ToString();
            SaveCoins();
            //for heart
            HeartTextGameOver.text = (LoadHeart()).ToString();
            // for score
            ScoreGameOver.text = "Score:" + (Score).ToString();
            SaveHighScore();
            HighScoreGOPanel.text = (LoadHighScore()).ToString();

        }

    }
    private void OnTriggerEnter(Collider colInfo)
    {
        if (colInfo.gameObject.CompareTag("Coins"))
        {
            Coins++;
            CoinsText.text = "X" + (Coins).ToString();

            Destroy(colInfo.gameObject);
            AudioManager.instance.Play("Coins");
        }
        if (colInfo.gameObject.CompareTag("Heart"))
        {
            int heartTemp = LoadHeart();
            heartTemp++;
            HeartText.text = "X" + (heartTemp).ToString();
            PlayerPrefs.SetString("TotalHeart", Helper.Encrypt(Helper.Serialize<int>(heartTemp)));

            Destroy(colInfo.gameObject);
            GameObject.FindObjectOfType<HeartManager>().SpawnHeart();
            AudioManager.instance.Play("Heart");
        }

    }
    // for continue
    public GameObject ContinuePanel;
    public GameObject TouchPanelMenu;
    public GameObject GameOverMenuPanel;
    private GameObject ObstacleToDestroy;
    private int continueCount = 0;
    private int continueValue = 1;
    public void GameContinue()
    {
        AudioManager.instance.Play("Click");
        movement.enabled = true;
        int TotalHeart = LoadHeart();

        ContinuePanel.SetActive(false);
        Time.timeScale = 1f;
        // mmoving back the obstacleToDestroy;
        Vector3 temp = ObstacleToDestroy.transform.position ;
        temp.z -= 15f;
        ObstacleToDestroy.transform.position = temp;

        TotalHeart = TotalHeart - continueValue;
        PlayerPrefs.SetString("TotalHeart", Helper.Encrypt(Helper.Serialize<int>(TotalHeart)));

        TouchPanelMenu.SetActive(true);
        HeartText.text = "X" + (LoadHeart()).ToString();
        playerAnim.Play("Running");
    }
    public void Skipfn()
    {
        AudioManager.instance.Play("Click");
        ContinuePanel.SetActive(false);
        GameOverMenuPanel.SetActive(true);

        // for coins
        CoinsTextGameOver.text = "+" + (Coins).ToString();
        SaveCoins();
        // for Heart
        HeartTextGameOver.text = (LoadHeart()).ToString();
        // for score
        ScoreGameOver.text = "Score:" + (Score).ToString();
        SaveHighScore();
        HighScoreGOPanel.text = (LoadHighScore()).ToString();
    }

    private void SaveCoins()
    {
        // in a begining TotalCoins set to zero with hash code
        int TotalCoins = Helper.Deserialize<int>(Helper.Decrypt(PlayerPrefs.GetString("TotalCoins", "Dpg2sFGa+1la3wlTQlu2OUBTc0g+GeByxSRP+ndnbFrQ5PAH07Ia5aIBHe65X7XrOgV5DRCtU3Q=")));
        TotalCoins += Coins;
        PlayerPrefs.SetString("TotalCoins", Helper.Encrypt(Helper.Serialize<int>(TotalCoins)));
        // PlayerPrefs.Save();

    }
    public int LoadCoin()
    {
        if (PlayerPrefs.HasKey("TotalCoins"))
        {
            // Debug.Log(PlayerPrefs.GetString("TotalCoins"));
            return Helper.Deserialize<int>(Helper.Decrypt(PlayerPrefs.GetString("TotalCoins")));

        }
        else
        {
            // int TotalCoins = PlayerPrefs.GetString("TotalCoins");
            SaveCoins();
            // Debug.Log("No Save file found, creating a new one!");
            return 0;
        }

    }
    
    public int LoadHeart()
    {
        if (PlayerPrefs.HasKey("TotalHeart"))
        {
            return Helper.Deserialize<int>(Helper.Decrypt(PlayerPrefs.GetString("TotalHeart")));
        }
        else
        {
            SaveCoins();
            return 0;
        }

    }
    private void SaveHighScore()
    {
        // in a begining highScore set to zero with hash code
        int HighScore = Helper.Deserialize<int>(Helper.Decrypt(PlayerPrefs.GetString("HighScore", "Dpg2sFGa+1la3wlTQlu2OUBTc0g+GeByxSRP+ndnbFrQ5PAH07Ia5aIBHe65X7XrOgV5DRCtU3Q=")));

        if (Score > HighScore)
        {
            PlayerPrefs.SetString("HighScore", Helper.Encrypt(Helper.Serialize<int>(Score)));
        }
    }
    public int LoadHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            return Helper.Deserialize<int>(Helper.Decrypt(PlayerPrefs.GetString("HighScore")));
        }
        else
        {
            SaveCoins();
            return 0;
        }

    }

}
