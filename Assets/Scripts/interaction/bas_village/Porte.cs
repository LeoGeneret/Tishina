using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte : Interactable
{
    public Transform door;

    public float smoothTime = 1.3f;
    public float endPos = -74.9f;

    protected new void Start()
    {
        base.Start();
    }

    void UpdateDoor()
    {
        StartCoroutine(Rotate(Vector3.up, endPos, smoothTime));
    }

    IEnumerator Rotate(Vector3 axis, float angle, float duration = 1.0f)
    {
        Quaternion from = door.rotation;
        Quaternion to = door.rotation;
        to *= Quaternion.Euler(axis * angle);

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            door.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        door.rotation = to;
    }

    public override void Interact()
    {
        UpdateDoor();
    }
}