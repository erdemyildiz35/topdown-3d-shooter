using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;
using Random = UnityEngine.Random;

public class GunController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    private AudioSource _audioSource;
    private Animator _anim;

    [SerializeField] private TextMeshProUGUI remainLaser;

    public int maxAmmo = 5;
    private int _currentAmmo;
    public float reloadTime = 1f;
    private bool _isReloading = false;
    private bool _canShoot = true;
    [SerializeField] private AudioClip _reloadingSound;
    
    [SerializeField] private GameObject[] ammoImages;

    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        _anim = GetComponentInParent<Animator>();

        _currentAmmo = maxAmmo;

        remainLaser.gameObject.SetActive(false);
        foreach (var ammoImage in ammoImages)
        {
            ammoImage.SetActive(true);
        }
    }

    void Update()
    {
        if (_isReloading)
        {
            return;
        }

        if (_currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (_canShoot)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_currentAmmo > 0)
                {
                    // PLAY SOUND
                    _audioSource.pitch = Random.Range(0.95f, 1.05f);
                    _audioSource.clip = Resources.Load<AudioClip>($"Shoot/Hit{Random.Range(1, 5)}");
                    _audioSource.Play();

                    // _anim.Play("Armature|attack");
                    StartCoroutine(Shoot());
                }
            }
        }
    }

    IEnumerator Shoot()
    {
        _canShoot = false;
        _currentAmmo--;

        ammoImages[_currentAmmo].SetActive(false);

        // _anim.SetBool("IsAttack", true);
        _anim.Play("Armature|attack");

        Instantiate(bullet, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.264f);

        //_anim.SetBool("IsAttack", false);

        _canShoot = true;
    }

    private IEnumerator Reload()
    {
        _isReloading = true;
        remainLaser.gameObject.SetActive(true);
        //remainLaser.text = "RELOADING";
        
        _audioSource.PlayOneShot(_reloadingSound);
        yield return new WaitForSeconds(reloadTime);

        foreach (var ammoImage in ammoImages)
        {
            ammoImage.SetActive(true);
        }

        remainLaser.gameObject.SetActive(false);

        _currentAmmo = maxAmmo;
        _isReloading = false;
    }
}