using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatform : MonoBehaviour
{
    public float timer = 2;
    public float respawnTimer = 5;
    public bool respawn = true;
    public Vector3 origin;

    public bool falling = false;
    public bool fall = false;

    float realTimer = 0;
    MovingPlatform mp;

    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        mp = GetComponent<MovingPlatform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mp.player != null)
        {
            fall = true;
        }
        if (fall)
        {
            if(realTimer > timer)
            {
                if(!falling)
                    Fall();
            }
            if(realTimer > timer + respawnTimer)
            {
                Respawn();
                realTimer = 0;
                falling = false;
                fall = false;
            }
            realTimer += Time.deltaTime;
        }
    }

    void Fall()
    {
        gameObject.AddComponent<Rigidbody>().drag = 0;
        gameObject.GetComponent<MeshCollider>().enabled = false;
        if(gameObject.GetComponent<MovingPlatform>() != null)
        {
            gameObject.GetComponent<MovingPlatform>().enabled = false;
        }
        origin = gameObject.transform.position;
        falling = true;
    }

    void Respawn()
    {
        Destroy(gameObject.GetComponent<Rigidbody>());
        gameObject.GetComponent<MeshCollider>().enabled = true;
        if (gameObject.GetComponent<MovingPlatform>() != null)
            gameObject.GetComponent<MovingPlatform>().enabled = true;
        gameObject.transform.position = origin;
    }
}
