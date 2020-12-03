using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JardinPorte : Interactable
{
    public GameObject porteMesh;
    public GameObject aura;

    public float smoothTime = 1.3f;
    public float endPos = 135f;

    protected new void Start()
    {
        base.Start();
    }

    void UpdateDoor()
    {
        StartCoroutine(Rotate(Vector3.up, endPos, smoothTime));
        StartCoroutine(Scale(aura, new Vector3(0, 0, 0), smoothTime / 3));

        porteMesh.GetComponent<Collider>().enabled = false;
        transform.GetComponent<Collider>().enabled = false;
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