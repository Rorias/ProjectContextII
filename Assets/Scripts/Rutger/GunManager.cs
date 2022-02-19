using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public Animator anim;
    public ParticleSystem testParticles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Aiming", Input.GetMouseButton(1));

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Shoot");
            testParticles.Stop();
            testParticles.Play();
        }
    }
}
