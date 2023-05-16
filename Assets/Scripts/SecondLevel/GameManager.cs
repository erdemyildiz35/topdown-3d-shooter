using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject firstTimeline;
    [SerializeField] private GameObject unstableInstructions;
    [SerializeField] private GameObject batteriesCollectedInstructions;
    [SerializeField] private GameObject playerDiedInstructions;
    [SerializeField] private GameObject playerGameObject;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject am = new GameObject("GameManager");
                am.AddComponent<GameManager>();
            }

            return _instance;
        }
    }
    
    [SerializeField] private Image playerHealthSliderImageFill;
    [SerializeField] private Image energyLevelSliderImageFill;
    [SerializeField] private Image stabilityLevelSliderImageFill;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip okyanus;

    public int killedEnemyNumbers = 0;
    public int collectedBatteryNumber = 0;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        energyLevelSliderImageFill.fillAmount = 0f;
        stabilityLevelSliderImageFill.fillAmount = 0f;
    }

    private void Update()
    {
        if (stabilityLevelSliderImageFill.fillAmount <= 0)
        {
            stabilityLevelSliderImageFill.fillAmount = 0;
        }
        
        if ( energyLevelSliderImageFill.fillAmount>=1)
        {
            StartCoroutine(AllBatteriesCollected());
        }

        CheckPlayersHealth();
        CheckStabilityLevels();
        // FillButtons();
    }

    public void CheckStabilityLevels()
    {
        if (stabilityLevelSliderImageFill.fillAmount  >= 1)
        {
            StartCoroutine(UnstableGameOver());
        }
    }

    IEnumerator AllBatteriesCollected()
    {
        batteriesCollectedInstructions.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("GameFinish");
    }
    IEnumerator UnstableGameOver()
    {
        unstableInstructions.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("FirstLevel");
    }

    public void CheckPlayersHealth()
    {
        if (playerHealthSliderImageFill.fillAmount <=0.01f)
        {
            StartCoroutine(PlayerDied());
        }
    }

    IEnumerator PlayerDied()
    {
        playerGameObject.SetActive(false);
        playerDiedInstructions.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("FirstLevel");
    }

    public void IncreaseCollectedBattery()
    {
        energyLevelSliderImageFill.fillAmount += 0.2f;
        collectedBatteryNumber++;
    }

    public void IncreaseStability(float amount)
    {
        stabilityLevelSliderImageFill.fillAmount += amount;
    }

    public void DecreaseStability(float amount)
    {
        stabilityLevelSliderImageFill.fillAmount -= amount;

    }
    
    public void PlayBackgroundMusic()
    {
        _audioSource.Play();
    }
    
    // public void FillButtons()
    // {
    //     if (Input.GetKey(KeyCode.E))
    //     {
    //         eButton.fillAmount = eButton.fillAmount + 0.01f ;
    //         if (eButton.fillAmount >= 0.95f)
    //         {
    //             print("Completed");
    //         }
    //     }
    //     else
    //     {
    //         eButton.fillAmount = eButton.fillAmount - 0.01f ;
    //
    //     }
    // }
}