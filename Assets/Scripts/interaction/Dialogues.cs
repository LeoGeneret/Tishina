using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dialogues : MonoBehaviour
{
    public GameObject Dialogue;
    public GameObject DialogueOverlay;
    public float duration = 2f;
    public bool repeteable = false; 
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collider");
        Dialogue.SetActive(true);
        DialogueOverlay.SetActive(true);

        StartCoroutine(PopupDuration());
    }

    IEnumerator PopupDuration()
    {
        yield return new WaitForSeconds(duration);

        DialogueOverlay.SetActive(false);
        Dialogue.SetActive(false);


        if(!repeteable)
        {
        transform.GetComponent<Collider>().enabled = false;
        }
    }
}