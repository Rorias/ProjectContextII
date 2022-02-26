using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement: MonoBehaviour
{
    public string eventName;
    public string publicName;
    public Texture2D icon;
    public int amount = 0;
    public int needed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isFinished()
    {
        if (amount >= needed) return true;
        return false;
    }

    public void eventCallback()
    {
        amount += 1;
        PlayerPrefs.SetInt("Achievement_" + eventName, amount);
    }
}
