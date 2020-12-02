using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public GameObject popUp;
    public GameObject iconWhenClose;
    public GameObject iconWhenOpen;


    public void show()
    {
        popUp.SetActive(true);
        iconWhenClose.SetActive(false);
        iconWhenOpen.SetActive(true);
    }

    public void hide()
    {
        popUp.SetActive(false);
        iconWhenClose.SetActive(true);
        iconWhenOpen.SetActive(false);
    }
}
