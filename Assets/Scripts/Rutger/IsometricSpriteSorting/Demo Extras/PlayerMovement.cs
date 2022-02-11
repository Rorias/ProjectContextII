using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// The purpose of this script is to set the player movement by overriding the rigidbody velocity.
    /// This is just for demostration purposes and not useful for the core idea of the project.
    /// Feel free to delete this script.
    /// </summary>

    public Vector2 scaleVector = new Vector2(1,1);

    public bool fourDir1 = false;
    public bool fourDir2 = false;

    Vector2 lastDir = new Vector2();

    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Rigidbody2D rigidBody2D;

    private int HASH_DIR_X = Animator.StringToHash("DirX");
    private int HASH_DIR_Y = Animator.StringToHash("DirY");
    private int HASH_WALK_MULTI = Animator.StringToHash("WalkSpeedMultiplier");
    private const float playerSpeed = 1.5f;

    private enum PlayerState {IDLE,MOVING};
    private PlayerState currentState;
    void Start ()
    {
        currentState = PlayerState.IDLE;
    }
	
	void Update ()
    {
        float inputX = Input.GetAxis("Horizontal") * scaleVector.x;
        float inputY = Input.GetAxis("Vertical") * scaleVector.y;       
        anim.SetFloat(HASH_DIR_X, inputX);
        anim.SetFloat(HASH_DIR_Y, inputY);

        

        if (inputX == 0 && inputY == 0)
        {
            //Debug.Log("Idleing: " + lastDir);
            anim.SetFloat(HASH_DIR_X, lastDir.x);
            anim.SetFloat(HASH_DIR_Y, lastDir.y);
        }
        else
        {
            //Debug.Log("setting lastDir: "+lastDir);
            
        }
        if (fourDir1)
        {
            if (inputX > 0)
            {
                inputX = 1;
                inputY = 0;
            }
            if (inputX < 0)
            {
                inputX = -1;
                inputY = 0;
            }
            if (inputY > 0)
            {
                inputY = 1;
                inputX = 0;
            }
            if (inputY < 0)
            {
                inputY = -1;
                inputX = 0;
            }
        }
        if (fourDir2)
        {
            if (inputX > 0)
            {
                inputX = 1;
                inputY = 1;
            }
            else if (inputX < 0)
            {
                inputX = -1;
                inputY = -1;
            }
            else if (inputY > 0)
            {
                inputY = 1;
                inputX = -1;
            }
            else if (inputY < 0)
            {
                inputY = -1;
                inputX = 1;
            }
            inputX *= scaleVector.x;
            inputY *= scaleVector.y;
        }

        //RUN
        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetFloat(HASH_WALK_MULTI, 1.8f);
            inputX *= 1.8f;
            inputY *= 1.8f;
        }
        else
            anim.SetFloat(HASH_WALK_MULTI, 1f);

        Vector2 finalVelocity = (new Vector2(inputX, inputY) ) * playerSpeed;
        if(finalVelocity.magnitude > 0)
            lastDir = finalVelocity.normalized;
        if (currentState == PlayerState.IDLE && finalVelocity.magnitude > 0.0f)
        {
            anim.SetTrigger("Move");
            currentState = PlayerState.MOVING;
        }
        else if (currentState == PlayerState.MOVING && finalVelocity.magnitude == 0.0f)
        {
            anim.SetTrigger("Idle");
            currentState = PlayerState.IDLE;
        }
        rigidBody2D.velocity = finalVelocity;
    }
}
