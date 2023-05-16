using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Battery : MonoBehaviour
{
    [SerializeField] private Image _eButtonImage;
    [SerializeField] private LayerMask layerMask;
    [Range(0, 10f)] [SerializeField] private int collisionCheckRange = 4;

    [SerializeField] private UnityEngine.UI.Image eButton;

    private bool _isButtonActive = true;

    [SerializeField] private GameObject[] spawnableEnemies;
    [SerializeField] private GameObject[] spawnLocations;

    [SerializeField] private TextMeshProUGUI killAmountText;
    [SerializeField] private GameObject batteryInstructionsText;
    private int _enemyNumberToCompleteLevel;

    private bool _isSpawningStarted = false;
    private bool _isBatterCollected = false;

    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        eButton.gameObject.SetActive(false);

        killAmountText.gameObject.SetActive(false);
        killAmountText.text = $"0/{spawnLocations.Length}";
    }

    private void OnEnable()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (!_isBatterCollected)
        {
            if (_isSpawningStarted && _enemyNumberToCompleteLevel == GameManager.Instance.killedEnemyNumbers)
            {
                _isBatterCollected = true;

                // Level Completed
                GameManager.Instance.IncreaseCollectedBattery();

                // particle effect
                Destroy(this.gameObject, 2f);
            }
        }

        if (Physics.CheckSphere(transform.position, collisionCheckRange, layerMask))
        {
            if (eButton != null) eButton.gameObject.SetActive(true);

            if (!_isButtonActive)
            {
                return;
            }

            FillButtons();
        }
        else
        {
            if (eButton != null) eButton.gameObject.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        killAmountText.text = $"{GameManager.Instance.killedEnemyNumbers}/{spawnLocations.Length}";
    }

    private void FillButtons()
    {
        if (Input.GetKey(KeyCode.E))
        {
            eButton.fillAmount = eButton.fillAmount + 0.005f;
            if (eButton.fillAmount >= 1f)
            {
                if (!_isSpawningStarted)
                {
                    GameManager.Instance.killedEnemyNumbers = 0;
                    _enemyNumberToCompleteLevel = spawnLocations.Length;

                    // YARATIKLAR SPAWN
                    GameManager.Instance.killedEnemyNumbers = 0;
                    StartCoroutine(SpawnEnemies(spawnableEnemies));
                    StartCoroutine(ShowBatteryInstructionText());
                    killAmountText.gameObject.SetActive(true);
                }


                _isButtonActive = false;
                eButton.gameObject.SetActive(false);
                _player.GetComponent<PlayerHealth>().health = 1f;
                Destroy(eButton);
            }
        }
        else
        {
            eButton.fillAmount = eButton.fillAmount - 0.005f;
        }
    }

    IEnumerator ShowBatteryInstructionText()
    {
        if (batteryInstructionsText)
        {
            batteryInstructionsText.SetActive(true);
            yield return new WaitForSeconds(10f);
            Destroy(batteryInstructionsText);
        }
        else
        {
            yield break ;
        }
    }

    IEnumerator SpawnEnemies(GameObject[] enemies)
    {
        // her bir spawn location icin 1 tane RANDOM enemy spawnla
        foreach (var spawnLocation in spawnLocations)
        {
            Instantiate(spawnableEnemies[Random.Range(0, 5)], spawnLocation.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(1f);
        }
        _isSpawningStarted = true;
    }
}