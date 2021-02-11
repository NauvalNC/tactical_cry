using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text scoreText, bestScoreText;

    private void Start()
    {
        int kills = PlayerPrefs.GetInt("kills", 0);
        int wave = PlayerPrefs.GetInt("wave", 0);

        scoreText.text = "Wave: " + wave.ToString() + "\nKills: " + kills.ToString();

        int bKills = PlayerPrefs.GetInt("bKills", 0);
        int bWave = PlayerPrefs.GetInt("bWave", 0);

        if (wave > bWave || kills > bKills) bestScoreText.gameObject.SetActive(true);

        if (bKills < kills) PlayerPrefs.SetInt("bKills", kills);
        if (bWave < wave) PlayerPrefs.SetInt("bWave", wave);

        SFXManager.Instance.Play("panel_entry");
    }

    public void MainMenu()
    {
        SceneLoader.sceneToLoad = "MainMenu";
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
    }
    
    public void Replay()
    {
        SceneLoader.sceneToLoad = "Gameplay";
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
    }
}
