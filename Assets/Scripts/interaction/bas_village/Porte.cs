using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte : Interactable
{
    public Transform door;
    public GameObject aura;

    public float smoothTime = 1.3f;
    public float endPos = -74.9f;

    protected new void Start()
    {
        base.Start();
    }

    void UpdateDoor()
    {
        StartCoroutine(Scale(aura, new Vector3(0, 0, 0), smoothTime / 3));
        StartCoroutine(Rotate(Vector3.up, endPos, smoothTime));
    }
    IEnumerator Scale(GameObject objectToScale, Vector3 scaleTo, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingScale = objectToScale.transform.localScale;
        while (elapsedTime < seconds)
        {
            objectToScale.transform.localScale = Vector3.Lerp(startingScale, scaleTo, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToScale.transform.position = scaleTo;
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