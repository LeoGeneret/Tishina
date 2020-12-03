using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        StartCoroutine(loadOutroDelay());
    }

    IEnumerator loadOutroDelay()
    {
        yield return new WaitForSeconds(7f);

        SceneManager.LoadScene("VideoOutro");
    }

    public override void Interact()
    {
        UpdateMachine();
    }
}
