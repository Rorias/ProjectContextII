using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeStickBasic : MonoBehaviour
{
    private GameObject player;
    //private Rigidbody rb;
    private ThirdPersonMovement TPM;

    private bool touched;
    private float distance;

    [Header("Settings")]
    public float minRange = 1.5f;
    public float stickyRange = 5;
    public float forcePower = 6;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        TPM = player.GetComponent<ThirdPersonMovement>();
        //rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);

        if (touched && distance > stickyRange)
        {
            TPM.SetMovePenalty(1f);
            touched = false;
        }

        if (!touched && distance <= minRange)
        {
            TPM.SetMovePenalty(0.5f);
            touched = true;
        }
    }
}
