using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCam : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 targetOffset;
    [SerializeField] private float MoveSpeed;

    // Update is called once per frame
    void Update()
    {
        MoveCam();
    }

    void MoveCam()
    {
        transform.position =
            Vector3.Slerp(transform.position, target.position + targetOffset, MoveSpeed * Time.deltaTime);
    }
}