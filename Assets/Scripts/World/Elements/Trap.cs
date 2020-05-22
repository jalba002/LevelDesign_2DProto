using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public static Transform spawnPoint;

    void Start()
    {
        if (spawnPoint == null)
            spawnPoint = GetComponent<SpawnPoint>().transform;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = spawnPoint.position;
        }
    }
}