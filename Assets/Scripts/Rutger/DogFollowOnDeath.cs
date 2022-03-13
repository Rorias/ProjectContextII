using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogFollowOnDeath : MonoBehaviour
{
    public Health health;
    public DogAI dog;

    // Start is called before the first frame update
    void Start()
    {
        health.deadCallbacks += StartFollow;
    }

    void StartFollow()
    {
        dog.target = FindObjectOfType<ThirdPersonMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
