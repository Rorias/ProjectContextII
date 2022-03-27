using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateSwitcher : MonoBehaviour
{
    public DogAI dogAI;

    public List<GameObject> enableOnDead;
    public List<GameObject> disableOnDead;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dogAI.dead)
        {
            foreach(GameObject obj in enableOnDead)
            {
                obj.SetActive(true);
            }

            foreach (GameObject obj in disableOnDead)
            {
                obj.SetActive(false);
            }
        }
    }
}
