using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioClip))]
public class SFXManager : MonoBehaviour
{
    static SFXManager instance;
    public static SFXManager Instance 
    {
        get { return instance = (!instance ? FindObjectOfType<SFXManager>() : instance); }
    }

    Dictionary<string, AudioClip> sfx = new Dictionary<string, AudioClip>();
    public Sound[] sfxClips;
    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();

        for (int i = 0; i < sfxClips.Length; i++)
        {
            sfx.Add(sfxClips[i].name, sfxClips[i].clip);
        }
    }

    public void Play(string name) 
    {
        source.clip = sfx[name];
        source.Play();
    }
}

[System.Serializable]
public class Sound 
{
    public string name;
    public AudioClip clip;
}
