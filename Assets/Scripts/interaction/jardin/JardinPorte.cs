using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JardinPorte : Interactable
{
    public GameObject porteMesh;

    public float smoothTime = 1.3f;
    public float endPos = 135f;

    protected new void Start()
    {
        base.Start();
    }

    void UpdateDoor()
    {
        StartCoroutine(Rotate(Vector3.up, endPos, smoothTime));

        porteMesh.GetComponent<Collider>().enabled = false;
    }

    IEnumerator Rotate(Vector3 axis, float angle, float duration = 1.0f)
    {
        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation;
        to *= Quaternion.Euler(axis * angle);

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = to;
    }

    public override void Interact()
    {
        UpdateDoor();
    }
}