using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static string sceneToLoad;
    public Image loadImage;

    private void Start()
    {
        AsyncOperation sync = SceneManager.LoadSceneAsync(sceneToLoad);
        loadImage.fillAmount = sync.progress;
    }
}
