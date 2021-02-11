using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMManager : MonoBehaviour
{
    AudioSource source;

    static BGMManager instance;
    public static BGMManager Instance
    {
        get 
        {
            if (instance == null) 
            {
                instance = FindObjectOfType<BGMManager>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        source = GetComponent<AudioSource>();
        source.Play();
        var temp = Instance;
    }
}
