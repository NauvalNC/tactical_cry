using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    [Header("Gameplay UI")]
    public Text killCount;
    public Text waveCount;
    public RectTransform hpBar;
    public float hpTime = 0.1f;
    public Animator pauseAC;

    [Header("Weapon")]
    public GameObject[] weapons;
    public Text weaponType;
    public Text weaponAmmo;

    private void Update()
    {
        GameplayUI();
        WeaponUI();

        if (Input.GetKeyDown(KeyCode.Escape)) Pause();
    }

    void GameplayUI() 
    {
        killCount.text = "Kill: " + GameManager.Instance.killCount.ToString();
        waveCount.text = "Wave " + GameManager.Instance.waveCount.ToString();
        hpBar.localScale = Vector2.Lerp(hpBar.localScale, new Vector2(Player.Instance.HP / Player.Instance.maxHP, hpBar.localScale.y), hpTime);
    }

    void WeaponUI() 
    {
        weaponType.text = (Player.Instance.gunStatus == 0) ? "Gun" : "Machine Gun";
        weaponAmmo.text = (Player.Instance.gunStatus == 0) ? "Infinity" : Player.Instance.ammo.ToString();
        foreach (GameObject obj in weapons) obj.SetActive(false);
        weapons[Player.Instance.gunStatus].SetActive(true);
    }

    public void Pause() 
    {
        StartCoroutine(IPause());
    }

    IEnumerator IPause() 
    {
        SFXManager.Instance.Play("button");
        if (pauseAC.gameObject.activeInHierarchy == false) pauseAC.gameObject.SetActive(true);
        pauseAC.SetBool("toggle", !pauseAC.GetBool("toggle"));

        int timeScale = pauseAC.GetBool("toggle") ? 0 : 1;
        if (timeScale == 0) yield return new WaitForSecondsRealtime(pauseAC.GetCurrentAnimatorStateInfo(0).length);
        Time.timeScale = timeScale;
    }

    public void Exit() 
    {
        Time.timeScale = 1;
        SceneLoader.sceneToLoad = "MainMenu";
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
    }
}
