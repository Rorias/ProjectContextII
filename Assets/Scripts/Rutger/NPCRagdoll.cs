using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class NPCRagdoll : MonoBehaviour
{
    Health health;
    Collider col;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        health.deadCallbacks += Ragdoll;

        col = GetComponent<Collider>();

        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Ragdoll()
    {
        col.enabled = false;

        if(gameObject.GetComponent<Animator>() != null)
            gameObject.GetComponent<Animator>().enabled = false;

        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
        }
    }
}
