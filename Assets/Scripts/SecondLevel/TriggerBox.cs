using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TriggerBox : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnableObjects;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnUnstableObjects();
            Destroy(this, 0.1f);
        }
    }

    private void SpawnUnstableObjects()
    {
        Instantiate(spawnableObjects[Random.Range(0, spawnableObjects.Length-1)],
            new Vector3(Random.Range(transform.position.x, transform.position.x), transform.position.y + 10f,
                Random.Range(transform.position.z, transform.position.z)),
            Random.rotation
        );
        GameManager.Instance.IncreaseStability(0.2f);
    }
}
