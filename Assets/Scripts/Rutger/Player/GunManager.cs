using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public Animator anim;
    public ParticleSystem testParticles, axeBlood;
    public GameObject hitpos;

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
        }
    }

    public void BatHit()
    {
        Debug.Log("Bat Hit!");
        //var batpos
        foreach(Collider col in Physics.OverlapSphere(hitpos.transform.position, .5f))
        {
            if(col.GetComponent<Health>() != null)
            {
                Debug.Log("Had health");
                col.GetComponent<Health>().Damage(10);
                axeBlood.Stop();
                axeBlood.Play();
            }
        }
    }

    public void ShotgunShoot()
    {
        Debug.Log("Bang!");
        testParticles.Stop();
        testParticles.Play();
    }
}
