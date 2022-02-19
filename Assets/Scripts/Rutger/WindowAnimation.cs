using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowAnimation : MonoBehaviour
{
    public Animation anim;
    public List<AnimationClip> animationClips;
    public float maxDelay = 15f;
    public float minDelay = 10f;

    float delay = 5f;
    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach(AnimationClip clip in animationClips)
        {
            anim.AddClip(clip, i.ToString());
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > delay)
        {
            timer = 0;
            delay = Random.Range(minDelay, maxDelay);
            anim.Play(Random.Range(0, animationClips.Count - 1).ToString());
        }

        if(!anim.isPlaying)
            timer += Time.deltaTime;
    }
}
