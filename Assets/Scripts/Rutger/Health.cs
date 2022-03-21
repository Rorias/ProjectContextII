using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;

    private float value = 20;

    public Action deadCallbacks;

    private void Start()
    {
        value = maxHealth;
    }

    public bool dead()
    {
        return value <= 0;
    }

    public void Damage(float value)
    {
        this.value -= value;
        if (dead()) deadCallbacks();
    }

    public void Heal(float value)
    {
        this.value = Mathf.Min(this.value + value, maxHealth);
    }

    public float GetValue()
    {
        return value;
    }
}
