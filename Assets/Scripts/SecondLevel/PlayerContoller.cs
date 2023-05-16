using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody _rb;
    private Animator _anim;
    private AudioSource _audioSource;

    [SerializeField] private Camera cameraCm;
    [SerializeField] private LayerMask layerMask;

    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Movement();
        RotateHandler();
    }

    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        Vector3 movement = new Vector3(horizontal, 0, vertical);
        //movement.Normalize();
        Vector3 moveVelocity = movement * speed;


        _rb.MovePosition(_rb.position + moveVelocity * Time.deltaTime);


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            _anim.SetBool("IsMove", true);
        }
        else
        {
            _anim.SetBool("IsMove", false);
        }
    }

    void RotateHandler()
    {
        Ray ray = cameraCm.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, layerMask))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(PlayBackgroundMusic());
        StartCoroutine(PlayThemeMusic());
    }

    IEnumerator PlayBackgroundMusic()
    {
        yield return new WaitForSeconds(10f);

        GameManager.Instance.PlayBackgroundMusic();
    }

    IEnumerator PlayThemeMusic()
    {
        yield return new WaitForSeconds(15f);

        AudioSecondLevel.Instance.PlayBackgroundMusic();
    }
}