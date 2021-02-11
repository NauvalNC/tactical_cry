using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Animator mainMenu, creditPanel, howToPlayPanel;
    public Text bestScoreText;

    private void Start()
    {
        int kills = PlayerPrefs.GetInt("bKills", 0);
        int wave = PlayerPrefs.GetInt("bWave", 0);

        if (kills != 0 && wave != 0)
        {
            bestScoreText.text = "Wave " + wave.ToString() + ", Kills " + kills.ToString();
        }
    }

    public void Play() 
    {
        StartCoroutine(IPlay());
    }

    public void Exit() 
    {
        Application.Quit();
    }

    public void HowToPlay() 
    {
        PlaySound();
        mainMenu.SetBool("toggle", !mainMenu.GetBool("toggle"));
        if (howToPlayPanel.gameObject.activeInHierarchy == false) howToPlayPanel.gameObject.SetActive(true);
        howToPlayPanel.SetBool("toggle", !howToPlayPanel.GetBool("toggle"));
    }

    public void Credit() 
    {
        PlaySound();
        mainMenu.SetBool("toggle", !mainMenu.GetBool("toggle"));
        if (creditPanel.gameObject.activeInHierarchy == false) creditPanel.gameObject.SetActive(true);
        creditPanel.SetBool("toggle", !creditPanel.GetBool("toggle"));
    }

    IEnumerator IPlay()
    {
        PlaySound();
        mainMenu.SetBool("toggle", !mainMenu.GetBool("toggle"));
        yield return new WaitForSeconds(mainMenu.GetCurrentAnimatorStateInfo(0).length);
        SceneLoader.sceneToLoad = "Gameplay";
        SceneManager.LoadScene("Loading");
    }

    void PlaySound() 
    {
        SFXManager.Instance.Play("panel_entry");
    }
}
