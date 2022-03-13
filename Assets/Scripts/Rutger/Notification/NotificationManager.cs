using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    

    private static NotificationManager notificationManager;

    public static NotificationManager instance
    {
        get
        {
            if (!notificationManager)
            {
                notificationManager = FindObjectOfType(typeof(NotificationManager)) as NotificationManager;

                if (!notificationManager)
                {
                    Debug.LogError("There needs to be one active Notification Manager script on a GameObject in your scene.");
                }
                else
                {
                    notificationManager.Init();
                }
            }

            return notificationManager;
        }
    }

    void Init()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ShowNotification(string text)
    {
        FindObjectOfType<NotificationMaker>().ShowNotification(text);
    }
}
