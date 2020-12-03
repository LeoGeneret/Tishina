using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : Interactable
{
    public Animator machine;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
    }
    void UpdateMachine()
    {
        int destroyHash = Animator.StringToHash("destroy");
        machine.SetTrigger(destroyHash);
    }

    public override void Interact()
    {
        UpdateMachine();
    }
}
