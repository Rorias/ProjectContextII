using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerSpawnPoint : MonoBehaviour
{
    public Transform npcSpawn;
    public GameObject trigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(GameObject npc)
    {
        npc.transform.position = npcSpawn.position;
        trigger.SetActive(true);
        trigger.GetComponent<CutsceneTrigger>().enabled = true;
    }
}
