using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTimer : MonoBehaviour
{
    public float timer = 5f;
    public GameObject follow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        GetComponent<RectTransform>().anchoredPosition = Camera.allCameras[0].WorldToScreenPoint(follow.transform.position + (Vector3.up*2f));
    }
}
