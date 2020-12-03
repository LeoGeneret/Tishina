using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Interactable
{
    public Transform wheel;
    public Transform axis;

    public GameObject aura;

    private bool done = false;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
    }

    protected new void Update()
    {
        base.Update();

        if (done)
        {
            Vector3 position = axis.GetComponent<Renderer>().bounds.center;

            wheel.transform.RotateAround(position, Vector3.forward, -10f * Time.deltaTime);
        }

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
    // Update is called once per frame
    void UpdateWater()
    {
        done = true;
    }

    public override void Interact()
    {
        UpdateWater();
    }
}
