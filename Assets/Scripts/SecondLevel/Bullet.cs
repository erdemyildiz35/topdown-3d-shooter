using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 60f;
    [SerializeField] private float lifeTime = 7f;

    [SerializeField] private GameObject particleEffect;
    private float _timer;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
                var particleObj = Instantiate(particleEffect, transform.position, Quaternion.identity);
                Destroy(particleObj,3f);
          
            Destroy(this.gameObject);
        }

        if (other.CompareTag("Environment"))
        {
            Destroy(this.gameObject);
        }
    }
}