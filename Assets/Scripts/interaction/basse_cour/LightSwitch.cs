using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Interactable
{
    public Light m_Light; // im using m_Light name since 'light' is already a variable used by unity

    public float smoothTime = 1.3f;
    public float endPos = 180;
    public Animator roosterAnimator;

    public GameObject aura;

    protected new void Start()
    {
        base.Start();
    }

    void UpdateLight()
    {
        int liveHash = Animator.StringToHash("live");
        roosterAnimator.SetTrigger(liveHash);

        StartCoroutine(Rotate(Vector3.up, endPos, smoothTime));

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