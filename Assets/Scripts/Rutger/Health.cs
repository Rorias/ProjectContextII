using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int startHealth = 20;

    int value = 20;

    public Action deadCallbacks;

    private void Start()
    {
        value = startHealth;
    }

    public bool dead()
    {
        return value <= 0;
    }

    public void Damage(int value)
    {
        this.value -= value;
        if (dead()) deadCallbacks();
    }

    public void Heal(int value)
    {
        this.value += value;
    }

    public int GetValue()
    {
        return value;
    }
}
