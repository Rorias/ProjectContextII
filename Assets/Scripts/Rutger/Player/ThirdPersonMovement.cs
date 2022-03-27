using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = System.Random;

public static class Vector2Extension
{

    public static Vector2 Rotate(this Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }
}
public class ThirdPersonMovement : MonoBehaviour
{

    [SerializeField] Transform cam;
    [SerializeField] GameObject cinemachineCam;
    [SerializeField] PlayerInput playerInput;

  //  [SerializeField] PlayerEffectsSync playerVFXSync;

    [SerializeField] AudioSource spawnAudioSource;
    [SerializeField] AudioSource musicAudioSource;

    [Header("Assignables")]
    public Transform groundCheck;
    public Transform bonkCheck;
    public LayerMask groundMask;
    public ParticleSystem moveEffect;
    public ParticleSystem jumpEffect;
    public Animator anim;
    //public Material mat;
    //public Material mat2;
    public AudioSource SFX;
    public AudioClip collectedSFX;
    public AudioClip[] dashSFX;
    public AudioClip[] jumpSFX;
    public AudioClip[] hurtSFX;
    public Health health;
    public GameObject deadUI;

    [Header("Settings")] 
    public float turnSmoothSpeed = 0.1f;
    public float normalSpeed = 6f;
    public float crouchSpeed = 3f;
    public float runSpeed = 8f;
    public float groundResistence = 8f;
    public float groundStoppingResistence = 8f;
    public float gravityMultiplier = 3f; // can be tweaked for flight zones
    public float jumpHeight = 3f;
    public bool doubleJump = true;
    public float groundDistance = 0.4f;
    public float platformDistance = 0.8f;
    public float jumpTime = 0.4f;
    public float airControl = 5.0f; // higher = less control, 1.0 is no effect
    public float airResistence = 8f;
    public float hangTime = 0.1f;
    public float jumpBuffer = 0.1f;
    public float dashSpeed = 3f;
    public float dashTime = 0.8f;
    public float dashTimeout = 3f;
    public Vector3 jumpEffectOffset = new Vector3(0, -1f, 0);
    public float slideSpeed = 6.0f;

    private CharacterController controller;

    private float turnSmoothVelocity;
    private float gravity = -9.81f;
    private float jumpTimeCounter;
    private float airDrag = 0;
    private float dashCounter;
    private float dashTimeoutCounter;

    private float hangCounter;
    private float jumpBufferCounter;
    private float movePenalty = 1.0f;
    private bool isPlayingMoveEffect = false;
    public bool isCrouching = false;
    public bool isHidden = false;

    private Vector3 velocity;
    public Vector3 velocityXZ { private set; get; }
    private Vector3 direction;
    private Vector3 dashDirection;
    private bool jumpInput;
    private bool dashInput;
    private bool interactInput;
    private bool isGrounded;
    private bool isOnTerrain;
    private bool isBonking;
    private bool isJumping;
    private bool hasDoubleJumped = false;
    //private MovingPlatform currentPlatform;
    //private TerrainDataHandler terrainDataHandler;

    private float playerAngle = 0;

    bool frozen = false;

    //public float interactRange;
    //[SerializeField]
    //private IInteractable iinteractableInRange;

    //PlayerManager playerManager;

    private void Awake()
    {

    }

    private void Start()
    {

        spawnAudioSource.Play();
        musicAudioSource.Play();

        //st = GameObject.FindGameObjectWithTag("singleton").GetComponent<Singleton>();
        controller = gameObject.GetComponent<CharacterController>();
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        PlayerPrefs.SetString("InteractKey", "E");

        health.deadCallbacks += Die;
    }

    void Die()
    {
        anim.SetBool("Dead", true);
        this.enabled = false;
        Time.timeScale = 0.3f;
        deadUI.SetActive(true);
    }

    public void Freeze()
    {
        frozen = true;
    }

    public void UnFreeze()
    {
        frozen = false;
    }

    private void FixedUpdate()
    {
        if (!frozen)
        {
            Gravity();
            BasicMovement();
        }
        MouseRot();
    }

    private void MouseRot()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        //Vector2 mousePos = new Vector2((Input.mousePosition.x / Screen.width) - 0.5f, (Input.mousePosition.y / Screen.height) - 0.5f);
        Vector2 mousePos = Input.mousePosition - screenPos;
        playerAngle = Vector2.SignedAngle(mousePos, new Vector2(-1, 1));
        //Debug.Log(playerAngle);
        transform.GetChild(0).rotation = Quaternion.Euler(0f, playerAngle, 0f);
        //Vector2 dir = direction;
        //Vector2 AnimDir = Vector2.Angle(dir, mousePos);
        Vector2 dir2 = new Vector2(direction.x, direction.z).Rotate(playerAngle-45f);
        if (!frozen)
        {
            anim.SetFloat("xSpeed", dir2.x * direction.magnitude);
            anim.SetFloat("ySpeed", dir2.y * direction.magnitude);
        }
        else
        {
            anim.SetFloat("xSpeed", 0);
            anim.SetFloat("ySpeed", 0);
        }
    }

    private void BasicMovement()
    {
        airDrag = (isGrounded) ? 1 : airControl; // 1 = no effect
        Vector3 dir = direction;

        //walk & run Animation
        anim.SetFloat("Speed", direction.magnitude);

        //Keep some velocity
        if (isGrounded)
            if(dir.magnitude > 0.1f) 
                velocityXZ -= velocityXZ * Mathf.Min(1, groundResistence * Time.deltaTime);
            else
                velocityXZ -= velocityXZ * Mathf.Min(1, groundStoppingResistence * Time.deltaTime);
        else
            velocityXZ -= velocityXZ * Mathf.Min(1, airResistence * Time.deltaTime);
        
        //Crouching
        if (isCrouching)
        {
            dir = dir * crouchSpeed;
            anim.SetFloat("Crouching", 1f);
            if (isHidden)
            {
                //Debug.Log("Hidden");
                //mat.SetFloat("Transparency", 1);
                //mat2.SetFloat("Transparency", 1);
            }
        }
        else
        {
            anim.SetFloat("Crouching", 0f);
            //mat.SetFloat("Transparency", 0);
            //mat2.SetFloat("Transparency", 0);
        }
        if (!isHidden)
        {
            //mat.SetFloat("Transparency", 0);
            //mat2.SetFloat("Transparency", 0);
        }

            //Dashing
        if (dashInput && dashTimeoutCounter <= 0 && isGrounded)
        {
            SFX.PlayOneShot(dashSFX[UnityEngine.Random.Range(0, dashSFX.Length - 1)]);
            dashTimeoutCounter = dashTimeout;
            dashCounter = dashTime;
            dashDirection = direction.normalized * dashSpeed;
            if(direction.normalized == Vector3.zero)
            {
                dashDirection = -transform.forward * dashSpeed;
            }
            isCrouching = false;
        }
        if (dashTimeoutCounter > 0) dashTimeoutCounter -= Time.deltaTime;
        if (dashCounter > 0)
        {
            dir = dashDirection;
            dashCounter -= Time.deltaTime;
            anim.SetBool("Dashing", true);
        }
        else
        {
            anim.SetBool("Dashing", false);
        }

        //Normal movement
        if (dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothSpeed * airDrag);


            transform.rotation = Quaternion.Euler(0f, playerAngle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            if (isGrounded)
                velocityXZ += moveDirection.normalized * dir.magnitude * runSpeed * movePenalty * Time.deltaTime * 30f;
            else
                velocityXZ += moveDirection.normalized * dir.magnitude * airControl * movePenalty * Time.deltaTime * 30f;
        }
        controller.Move((velocityXZ * Time.deltaTime));
    }

    private void Gravity()
    {
        float grav = gravity * gravityMultiplier;

        if (Physics.CheckSphere(groundCheck.position, groundDistance, groundMask, QueryTriggerInteraction.Ignore))
        {
            RaycastHit hit;
            Ray r = new Ray(groundCheck.position + Vector3.up, Vector3.down);
            if(Physics.Raycast(r, out hit, 20f, groundMask)){
                if(hit.normal.y < 0.5f)
                {
                    Vector3 cross = Vector3.Cross(hit.normal, Vector3.up);
                    Vector3 newDir = Vector3.Cross(hit.normal, cross);
                    controller.Move(newDir * slideSpeed * Time.deltaTime);
                }
            }
            if (!isGrounded && velocity.y < -12f) //We just landed and we hit fairly hard
            {
                jumpEffect.transform.position = transform.position + jumpEffectOffset;
                jumpEffect.Play();
            }
            isGrounded = true;
            hasDoubleJumped = false;
            anim.SetBool("Jumping", false);
            anim.SetBool("Falling", false);
        }
        else
        {
            isGrounded = false;
            isCrouching = false;
        }

        Collider[] cols = Physics.OverlapSphere(groundCheck.position, platformDistance, groundMask);
        bool onPlatform = false;
        isOnTerrain = false;
        foreach (Collider col in cols)
        {
            if (col.CompareTag("MovingPlatform"))
            {
                onPlatform = true;
            }
            if (col.CompareTag("Terrain"))
            {
                isOnTerrain = true;
            }
        }

        isBonking = Physics.CheckSphere(bonkCheck.position, groundDistance, groundMask, QueryTriggerInteraction.Ignore); //Did we bonk our head?

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if(!isGrounded)
        {
            controller.stepOffset = 0;
            anim.SetBool("Falling", true);
        }
        else
        {
            controller.stepOffset = 0.7f;
        }

        //Stop moving up if we bonk our head
        if (isBonking)
        {
            velocity.y = 0f;
            jumpTimeCounter = 0f;
        }

        //Allow player to still jump slightly after walking off a platform
        if (isGrounded)
            hangCounter = hangTime;
        else
            hangCounter -= Time.deltaTime;

        //Kick up some dust!
        if (isGrounded && controller.velocity.magnitude > 2f && direction.magnitude > 0.1f)
        {
            if(!isPlayingMoveEffect)
            {
                moveEffect.Play();
            }
            isPlayingMoveEffect = true;
        }
        else
        {
            moveEffect.Stop();
            isPlayingMoveEffect = false;
        }

        //Allow jumping from of a dash
        if (dashCounter > 0)
            hangCounter = hangTime;

        //jump buffer allows jump button to be pressed slightly early and still work
        if (jumpBufferCounter > 0) jumpBufferCounter -= Time.deltaTime;

        if (jumpBufferCounter > 0 && hangCounter > 0)
        {
            doJump();
        }

        if (jumpInput && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                jumpTimeCounter -= Time.deltaTime;
                grav *= 0.5f;
            }
            else
            {
                isJumping = false;
            }
        }
        
        if (!jumpInput)
        {
            isJumping = false;
        }

        velocity.y += grav * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void doJump()
    {
        SFX.PlayOneShot(jumpSFX[UnityEngine.Random.Range(0, jumpSFX.Length - 1)]);
        isJumping = true;
        jumpTimeCounter = jumpTime;
        // can do jump anim here
        anim.SetBool("Jumping", true);
        anim.SetBool("Falling", false);

        jumpEffect.transform.position = transform.position + jumpEffectOffset;
        jumpEffect.Play();

        velocity.y = jumpHeight;
        jumpBufferCounter = 0;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            direction = new Vector3(context.ReadValue<Vector2>().x, 0f, context.ReadValue<Vector2>().y).normalized;
            PlayerPrefs.SetString("InteractKey", "E");
        }
        else //Allow movement speed control on controllers
        {
            direction = new Vector3(context.ReadValue<Vector2>().x, 0f, context.ReadValue<Vector2>().y);
            PlayerPrefs.SetString("InteractKey", "Y");
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpBufferCounter = jumpBuffer;
            if (!isGrounded && !hasDoubleJumped)
            {
                hasDoubleJumped = true;
                doJump();
            }
        }
        jumpInput = context.performed;
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Started");
            SetMovePenalty(1.5f);
        }
        if (context.canceled)
        {
            Debug.Log("Cancelled");
            SetMovePenalty(1.0f);
        }
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        bool ignoreSecond = false;
        if(isCrouching && context.started)
        {
            isCrouching = false;
            ignoreSecond = true;
        }
        if (isGrounded && context.started && !isCrouching && !ignoreSecond)
        {
            isCrouching = true;
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        dashInput = context.performed;
    }
            
    ////iemand vervang dit even met iets fatsoenlijks volgensmij staat dit ook in het verkeerde script
    //public void OpenInventory(InputAction.CallbackContext context)
    //{
    //    if(GameManager.Instance.mode == GameManager.gameplayMode.Play || GameManager.Instance.mode == GameManager.gameplayMode.Inventory)
    //    {
    //        if (context.started)
    //        {
    //            Debug.Log("open Inventory");
    //            //GameManager.Instance.OpenInventory();
    //            //openInventoryInput = context.performed;
    //        }
    //    }

    //}

    //public void OpenMenu(InputAction.CallbackContext context)
    //{
    //    if (GameManager.Instance.mode == GameManager.gameplayMode.Play || GameManager.Instance.mode == GameManager.gameplayMode.EscMenu)
    //    {
    //        if (context.started)
    //        {
    //            Debug.Log("open Menu");
    //            //GameManager.Instance.OpenMenu();
    //            //openMenuInput = context.performed;
    //        }
    //    }
    //}

    //public void Interact(InputAction.CallbackContext context)
    //{ 

    //    if(context.started)
    //    {

    //        if (iinteractableInRange != null && GameManager.Instance.mode == GameManager.gameplayMode.Play || GameManager.Instance.mode == GameManager.gameplayMode.Talk)
    //        {
    //            iinteractableInRange.Action(this.gameObject);
    //            GameManager.Instance.interactableUI.SetActive(false);
    //        }
    //    interactInput = context.performed;
    //    }
    //}

    // 0.5 is half speed, 1.0 is normal speed, 2.0 is double speed
    public void SetMovePenalty(float value)
    {
        movePenalty = value;
    }

    private void OnTriggerExit(Collider other)
    {
        /*if (other.GetComponent<IInteractable>() != null)
        {
            GameManager.Instance.interactableUI.SetActive(false);

        }

        if (other.GetComponent<IInteractable>() != null && GameManager.Instance.mode == GameManager.gameplayMode.Talk)
        {
            other.GetComponent<Animator>().SetBool("isTalking", false);
            GameManager.Instance.SetGameplayMode(GameManager.gameplayMode.Play);
            GameManager.Instance.playerControllerCineCam.LookAt = GameManager.Instance.playerObject.transform;

        }

        //Debug.Log("I'm triggered!");
        if (other.CompareTag("Grass"))
        {
            //Debug.Log("We in the grass!");
            isHidden = false;
        }

        iinteractableInRange = null;*/
    }

    private void OnTriggerStay(Collider other)
    {
        /*
        if(other.GetComponent<IInteractable>() != null)
        {
            Debug.Log("Interactable in trigger");

            iinteractableInRange = other.gameObject.GetComponent<IInteractable>();
            GameManager.Instance.interactableUI.SetActive(true);
            GameManager.Instance.interactableNameUIText.text = iinteractableInRange.interactableName;
            GameManager.Instance.InteractableDescriptionUIText.text = iinteractableInRange.interactableDescription;

        }


        //interactableInRange = null;
        if (other.CompareTag("Grass"))
        {
            //Debug.Log("We in the grass!");
            isHidden = true;
        }*/
    }

    //private void ontriggere

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!isGrounded && hit.normal.y < 0.5f && velocity.y < 0f)
        {
            Vector3 cross = Vector3.Cross(hit.normal, Vector3.up);
            Vector3 newDir = Vector3.Cross(hit.normal, cross);
            Vector3 slideVelocity = newDir * -velocity.y;
            velocityXZ += new Vector3(slideVelocity.x, 0, slideVelocity.z) * Time.deltaTime;
        }
    }

    public bool GetStealthState()
    {
        return isHidden;
    }

    public bool GetCrouchState()
    {
        return isCrouching;
    }

    public bool IsOnTerrain()
    {
        if(isGrounded)
            return isOnTerrain;
        else
            return false;
    }

    public void GetHurt()
    {
        SFX.PlayOneShot(hurtSFX[UnityEngine.Random.Range(0, hurtSFX.Length - 1)]);
    }

    public void CollectedItem()
    {
        SFX.PlayOneShot(collectedSFX);
    }
}