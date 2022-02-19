using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 movement;
    public float time = 3;
    //public float stepSize = 0.1f;
    private Vector3 targetPos;
    private Vector3 originalPos;
    bool forward = true;
    public CharacterController player;
    private Vector3 step;
    private int steps = 0;
    private int stepTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        targetPos = transform.position + movement;
        step = (originalPos - targetPos) / time * Time.fixedDeltaTime;
        steps = (int)((originalPos - targetPos).magnitude / step.magnitude);
    }

    private void FixedUpdate()
    {
        if (forward)
        {
            //transform.position -= (originalPos - targetPos).normalized * speed * Time.fixedDeltaTime;
            transform.position -= step;
            if (player != null)
                //player.Move(-(originalPos - targetPos).normalized * speed * Time.fixedDeltaTime);
                player.Move(-step);
            stepTimer += 1;
        }
        else
        {
            transform.position += step;
            if (player != null)
                player.Move(step);
            stepTimer -= 1;
            /*transform.position += (originalPos - targetPos).normalized * speed * Time.fixedDeltaTime;
            if (player != null)
                player.Move((originalPos - targetPos).normalized * speed * Time.fixedDeltaTime);*/
        }
        /*if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            forward = false;
        if (Vector3.Distance(transform.position, originalPos) < 0.1f)
            forward = true;*/
        if (stepTimer > steps)
            forward = false;
        if (stepTimer < 0)
            forward = true;
    }
}
