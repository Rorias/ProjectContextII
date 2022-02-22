using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

[RequireComponent(typeof(Animator))]
public class CutsceneManager : MonoBehaviour
{
    PlayableGraph playableGraph;
    AnimationPlayableOutput playableOutput;

    // Start is called before the first frame update
    void Start()
    {
        playableGraph = PlayableGraph.Create();

        playableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);

        playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", GetComponent<Animator>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayClip(AnimationClip clip)
    {
        //anim.clip = clip;
        //anim.Play();
        Play(clip);
    }

    void Play(AnimationClip clip)

    {
        playableGraph.Stop();

        // Wrap the clip in a playable

        var clipPlayable = AnimationClipPlayable.Create(playableGraph, clip);

        // Connect the Playable to an output

        playableOutput.SetSourcePlayable(clipPlayable);

        // Plays the Graph.

        playableGraph.Play();

    }

    void OnDisable()

    {

        // Destroys all Playables and PlayableOutputs created by the graph.

        playableGraph.Destroy();

    }
}
