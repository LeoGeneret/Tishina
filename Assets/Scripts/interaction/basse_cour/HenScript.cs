using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenScript : Interactable
{
    public Animator henAnimator;
    public GameObject aura;

    protected new void Start()
    {
        base.Start();
    }

    void UpdateHen()
    {
        int liveHash = Animator.StringToHash("live");
        henAnimator.SetTrigger(liveHash);

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

    public override void Interact()
    {
        UpdateHen();
    }
}