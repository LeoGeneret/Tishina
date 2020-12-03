using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : Interactable
{
    public Transform bridge;

    public float smoothTime = 1.3f;
    public float endPos = 52f;

    public GameObject aura;

    Vector3 rotationSmoothVelocity;

    protected new void Start()
    {
        base.Start();
    }

    void UpdateBridge()
    {
        StartCoroutine(Rotate(Vector3.forward, endPos, smoothTime));

        StartCoroutine(Scale(aura, new Vector3(0, 0, 0), 3f / 3));
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
        Quaternion from = bridge.transform.rotation;
        Quaternion to = bridge.transform.rotation;
        to *= Quaternion.Euler(axis * angle);

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            bridge.transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        bridge.transform.rotation = to;
    }

    public override void Interact()
    {
        UpdateBridge();
    }
}