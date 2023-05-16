using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWithNav : MonoBehaviour
{
    [SerializeField] private float health = 20;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject playerSc;
    private NavMeshAgent _agent;
    private Animator _anim;

    
    void Start()
    {
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < 70f)
        {
            _anim.SetBool("Run", true);

            _agent.SetDestination(player.position);
        }
        else
        {
            _anim.SetBool("run", false);
        }

        if (Vector3.Distance(transform.position, player.position) < 7f)
        {
            StartCoroutine(attack());
        }
        else
        {
            _anim.SetBool("Attack", false);
            _anim.SetBool("Run", true);
        }

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            health -= 10;
        }
    }

    IEnumerator attack()
    {
        _anim.SetBool("Attack", true);
        player.gameObject.GetComponent<PlayerHealth>().TakeDamage();
        yield return new WaitForSecondsRealtime(2f);
    }
}