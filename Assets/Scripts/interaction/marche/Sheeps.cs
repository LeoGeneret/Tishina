using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheeps : Interactable
{
    public Animator movingSheep1;
    public Transform movingSheep1Transform;
    public GameObject movingSheep1Obj;
    
    public Animator movingSheep2;
    public Transform movingSheep2Transform;
    public GameObject movingSheep2Obj;

    public Animator movingSheep3;
    public Transform movingSheep3Transform;
    public GameObject movingSheep3Obj;

    public Animator movingSheep4;
    public Transform movingSheep4Transform;
    public GameObject movingSheep4Obj;

    public Animator movingSheep5;
    public Transform movingSheep5Transform;
    public GameObject movingSheep5Obj;

    public Animator staticSheep1;
    public GameObject staticSheep1Obj;

    public Animator staticSheep2;
    public GameObject staticSheep2Obj;

    public float duration = 2f;

    protected new void Start()
    {
        base.Start();
    }

    void UpdateSheep()
    {
        int walkHash = Animator.StringToHash("walk");
        movingSheep1.SetTrigger(walkHash);
        movingSheep2.SetTrigger(walkHash);
        movingSheep3.SetTrigger(walkHash);
        movingSheep4.SetTrigger(walkHash);
        movingSheep5.SetTrigger(walkHash);

        int idleHash = Animator.StringToHash("idle");
        staticSheep1.SetTrigger(idleHash);
        staticSheep2.SetTrigger(idleHash);        
        
        StartCoroutine(SmoothRecolor(movingSheep1Obj, 0, 1f, 2f));
        StartCoroutine(SmoothRecolor(movingSheep2Obj, 0, 1f, 2f));
        StartCoroutine(SmoothRecolor(movingSheep3Obj, 0, 1f, 2f));
        StartCoroutine(SmoothRecolor(movingSheep4Obj, 0, 1f, 2f));
        StartCoroutine(SmoothRecolor(movingSheep5Obj, 0, 1f, 2f));

        StartCoroutine(LerpPosition(movingSheep1Transform, movingSheep1, new Vector3(-3.33f, 0, -2.98f), duration));
        StartCoroutine(LerpPosition(movingSheep2Transform, movingSheep2, new Vector3(1.94f, 0, -4.55f), duration));
        StartCoroutine(LerpPosition(movingSheep3Transform, movingSheep3, new Vector3(0.08f, 0, -3.45f), duration));
        StartCoroutine(LerpPosition(movingSheep4Transform, movingSheep4, new Vector3(2.17f, 0, 0.21f), duration));
        StartCoroutine(LerpPosition(movingSheep5Transform, movingSheep5, new Vector3(2.5f, 0, -2.55f), duration));

        StartCoroutine(SmoothRecolor(staticSheep1Obj, 0, 1f, 2f));
        StartCoroutine(SmoothRecolor(staticSheep2Obj, 0, 1f, 2f));

        transform.GetComponent<Collider>().enabled = false;
    }
    IEnumerator LerpPosition(Transform obj, Animator anim, Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = obj.localPosition;

        while (time < duration)
        {
            obj.localPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        obj.localPosition = targetPosition;

        int idleHash = Animator.StringToHash("idle");
        anim.SetTrigger(idleHash);
    }

    IEnumerator SmoothRecolor(GameObject obj, float v_start, float v_end, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float speed = Mathf.Lerp(v_start, v_end, elapsed / duration);
            obj.GetComponent<Renderer>().material.SetFloat("Vector1_33C41BE9", speed);
            elapsed += Time.deltaTime;
            yield return null;
        }
        obj.GetComponent<Renderer>().material.SetFloat("Vector1_33C41BE9", v_end);
    }


    public override void Interact()
    {
        UpdateSheep();
    }
}