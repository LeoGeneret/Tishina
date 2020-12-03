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
    public void toHome()
    {
        Debug.Log("change scene");
        SceneManager.LoadScene("Home");
    }

    public void toLvl1()
    {
        PlayerAmmo.Ammo = 0f;
        Debug.Log("change scene");
        SceneManager.LoadScene("1_Basse_Cour");
    }
}
