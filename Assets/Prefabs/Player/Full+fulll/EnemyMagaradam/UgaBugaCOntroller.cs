using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class UgaBugaCOntroller : MonoBehaviour
{
    NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private GameObject _player;
    [SerializeField] float damageTakenAmount = 10;
    [SerializeField] float ugaBugaHealth = 50;

    AudioSource _audioSource;
    float _attackTimer = 3f;
    [SerializeField] AudioClip attackSoundClip, run;

    private bool _isDead = false;
    private bool _isDealingDamage = true;
    

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _isDead = false;
    }

    void Update()
    {
        _attackTimer -= .01f;

        if (Vector3.Distance(_player.transform.position, transform.position) < 140f)
        {
            _animator.SetBool("Run", true);
            _navMeshAgent.SetDestination(_player.transform.position);
        }
        else
        {
            _animator.SetBool("Run", false);
        }

        if (Vector3.Distance(_player.transform.position, transform.position) < 7f)
        {
            _animator.SetBool("attack", true);

            if (_isDealingDamage)
            {
                _isDealingDamage = false;


                _audioSource.PlayOneShot(attackSoundClip);
                StartCoroutine(DealDamage());
            }
        }
        else
        {
            _animator.SetBool("attack", false);
        }

        if (!_isDead)
        {
            if (ugaBugaHealth <= 0)
            {
                _isDead = true;
                GameManager.Instance.killedEnemyNumbers++;

                _animator.SetBool("die", true);
                _navMeshAgent.isStopped = true;

                Destroy(this.gameObject, 2f);
            }
        }
    }

    IEnumerator DealDamage()
    {
        _player.GetComponent<PlayerHealth>().TakeDamage();
        yield return new WaitForSeconds(1);

        _isDealingDamage = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            ugaBugaHealth -= damageTakenAmount;
        }
        else
        {
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            ugaBugaHealth -= damageTakenAmount;
        }
        else
        {
        }
    }

    IEnumerator attack()
    {
        _animator.SetBool("attack", true);
        _player.GetComponent<PlayerHealth>().TakeDamage();

        yield return new WaitForSeconds(2f);
    }
}