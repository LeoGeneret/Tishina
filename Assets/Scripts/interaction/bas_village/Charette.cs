using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charette : Interactable
{
    public Transform charette;

    public float smoothTime = 1.3f;
    public float endPos = 62f;

    protected new void Start()
    {
        base.Start();
    }

    void UpdateCharette()
    {
        Vector3 movePos = new Vector3(charette.position.x, charette.position.y, endPos);

        /*charette.position = Vector3.Lerp(charette.position, movePos, Time.deltaTime * smoothTime);
        Debug.Log(charette.position);*/

        StartCoroutine(LerpPosition(movePos, smoothTime));
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = charette.position;

        while (time < duration)
        {
            charette.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        charette.position = targetPosition;
    }

    public override void Interact()
    {
        UpdateCharette();
    }
}