﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int dmg = 100;

    public int GetDmg()
    {
        return dmg;
    }
    public void Hit()
    {
        Destroy(gameObject);
    }
}
