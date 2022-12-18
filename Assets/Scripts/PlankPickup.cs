using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            GameManager.instance.OnPlankPickup();
    }
}
