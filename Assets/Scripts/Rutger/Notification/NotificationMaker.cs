using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationMaker : MonoBehaviour
{
    public GameObject notificationPrefab;
    public GameObject canvas;

    GameObject lastNotification;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowNotification(string text)
    {
        GameObject obj = Instantiate(notificationPrefab, canvas.transform);
        obj.GetComponent<Notification>().previousNotification = lastNotification;
        obj.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = text;
        lastNotification = obj;
    }
}
