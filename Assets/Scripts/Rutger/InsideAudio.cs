using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class InsideAudio : MonoBehaviour
{
    public AudioMixer mixer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            mixer.SetFloat("LowpassWet", 0);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            mixer.SetFloat("LowpassWet", -80);
    }
}
