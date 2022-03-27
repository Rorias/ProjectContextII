using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour
{
    public GameObject previousNotification;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (previousNotification)
        {
            Vector3 pos = GetComponent<RectTransform>().position;
            pos.y = previousNotification.GetComponent<RectTransform>().position.y - (previousNotification.GetComponent<RectTransform>().sizeDelta.y + 30);
            GetComponent<RectTransform>().position = pos;
        }
    }

    public void DestroyNotification()
    {
        Destroy(gameObject);
    }
}
