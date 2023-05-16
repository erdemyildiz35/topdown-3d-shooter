using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private Animator _animator;
    private static readonly int PlayCanvasAnim = Animator.StringToHash("PlayCanvasAnim");
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private AudioClip disabledSound;
    [SerializeField] private AudioClip jumpSound;

    public void JumpButton()
    {
        if (dropdown.value == 0)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(disabledSound);
        }
        else if (dropdown.value != 0)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(jumpSound);
            SceneManager.LoadScene("SecondLevel");
        }
    }

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    public void PlayButton()
    {
        _animator.SetBool(PlayCanvasAnim, true);
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("ExitScene");
    }
}