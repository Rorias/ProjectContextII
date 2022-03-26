using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    public AnimationClip clip;
    CutsceneManager cm;

    // Start is called before the first frame update
    void Start()
    {
        cm = FindObjectOfType<CutsceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cm.PlayClip(clip);
            try
            {
                EventManager.TriggerEvent("DialogOpen");
            }catch(Exception e)
            {

            }
            gameObject.SetActive(false);
        }
    }
}
