using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public GameObject objToFollow;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - objToFollow.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = objToFollow.transform.position + offset;
    }
}
