using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour
{
    public GameObject target;
    public NavMeshAgent agent;
    public Animator anim;

    public GameObject knife;
    public ParticleSystem knifeBlood;

    public Health health;

    public bool moving = false;
    public float speed = 1;
    public bool agro = false;

    public bool freezePlayerOnDeath = false;
    public bool dogFollowOnDeath = false;

    public bool disableOnStart = false;

    // Start is called before the first frame update
    void Start()
    {
        health.deadCallbacks += Die;
        StartCoroutine(SlowTick());
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if ((transform.position - target.transform.position).magnitude < 2f)
            {
                agent.speed = 0;
                moving = false;
                anim.SetFloat("SpeedY", 0);

                if (agro)
                {
                    transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
                    anim.SetBool("Attacking", true);
                }
            }
            else
            {
                agent.speed = speed;
                moving = true;
                anim.SetFloat("SpeedY", .8f);
                anim.SetBool("Attacking", false);
            }
        }
    }

    void Die()
    {
        if (freezePlayerOnDeath)
            FindObjectOfType<ThirdPersonMovement>().Freeze();
        if (dogFollowOnDeath)
            FindObjectOfType<DialogManager>().DogStartFollow();
        this.enabled = false;
        agent.enabled = false;
    }

    public void KnifeHit()
    {
        foreach (Collider col in Physics.OverlapSphere(knife.transform.position, .5f))
        {
            Debug.Log("Knife hit!"+col.name);
            if (col.GetComponent<Health>() != null && col.GetComponent<Health>() != health)
            {
                Debug.Log("Had health");
                col.GetComponent<Health>().Damage(10);
                knifeBlood.Stop();
                knifeBlood.Play();
                break;
            }
        }
    }

    IEnumerator SlowTick()
    {
        while (true)
        {
            if (target != null)
                agent.SetDestination(target.transform.position);
            yield return new WaitForSeconds(1);
        }
    }
}
