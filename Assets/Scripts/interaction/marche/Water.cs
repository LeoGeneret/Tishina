using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Interactable
{
    public Transform wheel;
    public Transform axis;

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
