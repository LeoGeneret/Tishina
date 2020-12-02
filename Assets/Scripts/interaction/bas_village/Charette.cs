using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charette : Interactable
{
    public Transform charette;
    public GameObject aura;
    public Animator horseAnimator;
    public GameObject horseGameObject;

    public float smoothTime = 1.3f;
    public float endPos = 62f;

    protected new void Start()
    {
        base.Start();
    }

    void UpdateCharette()
    {
        StartCoroutine(Scale(aura, new Vector3(0, 0, 0), smoothTime / 3));
        StartCoroutine(LerpPosition(new Vector3(322.18f,31.96f,76.21f), smoothTime));

        horseGameObject.GetComponent<Collider>().enabled = false;

        StartCoroutine(Walk());
    }


    IEnumerator Walk()
    {
        int walkHash = Animator.StringToHash("walk");
        horseAnimator.SetTrigger(walkHash);
        
        yield return new WaitForSeconds(smoothTime);

        int stopHash = Animator.StringToHash("stop");
        horseAnimator.SetTrigger(stopHash);
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

    public override void Interact()
    {
        UpdateCharette();
    }
}