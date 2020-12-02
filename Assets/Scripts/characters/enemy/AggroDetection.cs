using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AggroDetection : MonoBehaviour
{

    public event Action<Transform> OnAggro = delegate { };

    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            OnAggro(player.transform);
        }
    }
}
