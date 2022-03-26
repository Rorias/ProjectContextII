using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogAI : MonoBehaviour
{
    public GameObject target;
    public NavMeshAgent agent;
    public Animator anim;

    public bool moving = false;
    public float speed = 1;

    public bool dead = false;

    public GameObject namingTrigger;

    // Start is called before the first frame update
    void Start()
    {
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
            }
            else
            {
                agent.speed = speed;
                moving = true;
                anim.SetFloat("SpeedY", .8f);
            }
        }
    }

    IEnumerator SlowTick()
    {
        while (true)
        {
            if(target != null)
                agent.SetDestination(target.transform.position);
            yield return new WaitForSeconds(1);
        }
    }

    public void Die()
    {
        agent.enabled = false;
        anim.SetBool("Dead", true);
        dead = true;
    }

    public void StartNameSequence()
    {
        FindObjectOfType<CamFollow>().NewFollow(gameObject);
        namingTrigger.SetActive(true);
    }
}
