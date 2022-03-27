using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public EverloopController ec;
    public float volume = 0.2f;
    public int channels;

    // Start is called before the first frame update
    void Start()
    {
        ec.volume = volume;
        ec.StopAutopilot();
        ec.numActiveTracks = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
