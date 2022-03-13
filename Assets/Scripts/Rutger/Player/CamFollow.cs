using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    GameObject player;
    public GameObject objToFollow;
    public float speed = 2f;
    Vector3 target;
    Vector3 currentPos;
    Vector3 offset;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<ThirdPersonMovement>().gameObject;
        offset = transform.position - player.transform.position;
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        target = player.transform.position + offset;
        /*if(objToFollow != null)
        {
            target = objToFollow.transform.position + offset;
            speed = 5;
        }
        else
        {
            speed = 2;
        }*/
        transform.position -= (transform.position - target) / speed;
        //currentPos = transform.position;
    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            //Debug.Log("scroll " + Input.mouseScrollDelta);
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - Input.mouseScrollDelta.y * 0.5f, 3, 10);
        }
    }
}
