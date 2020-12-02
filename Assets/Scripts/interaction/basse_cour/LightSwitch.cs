using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Interactable
{
    public Light m_Light; // im using m_Light name since 'light' is already a variable used by unity

    public float smoothTime = 1.3f;
    public float endPos = 180;
    private Quaternion targetRotation;


    protected new void Start()
    {
        base.Start();
    }

    void UpdateLight()
    {
        StartCoroutine(Rotate(Vector3.up, endPos, smoothTime));
    }
    IEnumerator Rotate(Vector3 axis, float angle, float duration = 1.0f)
    {
        Quaternion from = m_Light.transform.rotation;
        Quaternion to = m_Light.transform.rotation;
        to *= Quaternion.Euler(axis * angle);

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            m_Light.transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        m_Light.transform.rotation = to;
    }

    public override void Interact()
    {
        UpdateLight();
    }
}