using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnedObjects : MonoBehaviour
{
    [Range(0, 10f)] [SerializeField] private int collisionCheckRange = 3;
    [SerializeField] private LayerMask layerMask;
    private Canvas _qButtonCanvas;
    private UnityEngine.UI.Image _qButtonImage;
    private bool _isButtonActive = true;
    
    private void Start()
    {
        _qButtonCanvas = gameObject.GetComponentInChildren<Canvas>();
        _qButtonImage = _qButtonCanvas.gameObject.GetComponentInChildren<UnityEngine.UI.Image>();
    }

    private void OnEnable()
    {
        _qButtonCanvas = gameObject.GetComponentInChildren<Canvas>();
        _qButtonImage = _qButtonCanvas.gameObject.GetComponentInChildren<UnityEngine.UI.Image>();
    }

    private void Update()
    {
        if (Physics.CheckSphere(transform.position, collisionCheckRange, layerMask))
        {
            _qButtonCanvas.gameObject.SetActive(true);

            if (!_isButtonActive)
            {
                return;
            }

            FillButtons();
        }
        else
        {
            _qButtonCanvas.gameObject.SetActive(false);
        }
    }

    private void FillButtons()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            _qButtonImage.fillAmount = _qButtonImage.fillAmount + 0.005f;
            if (_qButtonImage.fillAmount >= 1f)
            {
                _qButtonImage.gameObject.SetActive(false);
                
                // Play stability bar sound
                GameManager.Instance.DecreaseStability(0.2f);
                
                // Play particle effect
                Destroy(this.gameObject);

                _isButtonActive = false;
            }
        }
        else
        {
            _qButtonImage.fillAmount = _qButtonImage.fillAmount - 0.005f;
        }
    }
}