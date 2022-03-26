using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerSpawner : MonoBehaviour
{
    public List<KillerSpawnPoint> killerSpawnPoints = new List<KillerSpawnPoint>();
    public GameObject npc;
    public Achievement newspapers;
    public int newspapersNeeded;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckNewsPaper());
    }

    // Update is called once per frame
    IEnumerator CheckNewsPaper()
    {
        while (true)
        {
            if (newspapers.amount >= newspapersNeeded)
            {
                Vector3 playerPos = FindObjectOfType<ThirdPersonMovement>().transform.position;
                KillerSpawnPoint furthestksp = killerSpawnPoints[0];
                float bestDist = 0;

                foreach (KillerSpawnPoint ksp in killerSpawnPoints)
                {
                    if (Vector3.Distance(playerPos, ksp.npcSpawn.position) > bestDist)
                    {
                        furthestksp = ksp;
                        bestDist = Vector3.Distance(playerPos, ksp.npcSpawn.position);
                    }
                }

                furthestksp.Setup(npc);
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(1);
        }
    }
}
