using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    public float forceApplied = 10f;
    public Vector3 forceDirection;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(forceDirection * forceApplied, ForceMode.Impulse);
        }
    }
}