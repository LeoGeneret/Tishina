using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAmmo
{
    private static float ammo = 0;

    public static float Ammo
    {
        get
        {
            return ammo;
        }
        set
        {
            ammo = value;
        }
    }

}