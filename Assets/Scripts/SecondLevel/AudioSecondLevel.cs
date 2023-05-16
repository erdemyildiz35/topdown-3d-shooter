using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSecondLevel : MonoBehaviour
{
    
    private AudioSource _audioSource;

    private static AudioSecondLevel _instance;

    public static AudioSecondLevel Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject am = new GameObject("AudioSecondLevel");
                am.AddComponent<AudioSecondLevel>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlayBackgroundMusic()
    {
        _audioSource.Play();
    }
}
