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
    public bool completed = false;

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
        return completed;
    }

    public void eventCallback()
    {
        amount += 1;
        if(amount > needed)
        {
            amount = needed;
        }
        PlayerPrefs.SetInt("Achievement_" + eventName, amount);
        if (!completed)
        {
            if (amount >= needed)
            {
                NotificationManager.ShowNotification("Achievement " + publicName + " completed!");
                completed = true;
            }
            else
            {
                NotificationManager.ShowNotification("Achievement " + publicName + " " + amount + "/" + needed);
            }
        }
    }
}
