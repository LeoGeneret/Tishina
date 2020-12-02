using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : Interactable
{
    public Transform bridge;

    public float smoothTime = 1.3f;
    public float endPos = 52f;

    Vector3 rotationSmoothVelocity;

    protected new void Start()
    {
        base.Start();

        UpdateBridge();
    }

    void UpdateBridge()
    {
        StartCoroutine(Rotate(Vector3.forward, endPos, smoothTime));
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