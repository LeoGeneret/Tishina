using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenScript : Interactable
{
    public Animator henAnimator;

    protected new void Start()
    {
        base.Start();
    }

    void UpdateHen()
    {
        int liveHash = Animator.StringToHash("live");
        henAnimator.SetTrigger(liveHash);
    }

    public override void Interact()
    {
        UpdateHen();
    }
}