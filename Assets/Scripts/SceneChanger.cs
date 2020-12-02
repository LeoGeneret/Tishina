using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void toScene2()
    {
        Debug.Log("change scene");
        SceneManager.LoadScene("VideoIntro");
    }

    public void toLvl1()
    {
        Debug.Log("change scene");
        SceneManager.LoadScene("1_Basse_Cour");
    }
}
