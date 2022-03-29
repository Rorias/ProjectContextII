using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DogAI : MonoBehaviour
{
    public GameObject target;
    public NavMeshAgent agent;
    public Animator anim;

    public bool moving = false;
    public float speed = 1;

    public bool dead = false;

    public GameObject namingTrigger;
    public GameObject thoughtUI;
    public Image currentThought;

    public List<GameObject> enableOnDeath = new List<GameObject>();

    public List<GameObject> availableMapItems = new List<GameObject>();
    public List<Sprite> availableThoughts = new List<Sprite>();

    private float dogLoveTimer = 120.0f;
    private float maxDogLoveTime = 120.0f;

    private float currentThoughtTimer = 5.0f;
    private float thoughtCooldownTimer = 0.0f;
    private float maxThoughtCooldownTime = 10.0f;

    private void Start()
    {
        StartCoroutine(SlowTick());
    }

    private void Update()
    {
        if (target != null)
        {
            if ((transform.position - target.transform.position).magnitude < 2f)
            {
                agent.speed = 0;
                moving = false;
                anim.SetFloat("SpeedY", 0);
            }
            else
            {
                agent.speed = speed;
                moving = true;
                anim.SetFloat("SpeedY", .8f);
            }
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            thoughtUI.transform.LookAt(Camera.main.transform);
            dogLoveTimer -= Time.fixedDeltaTime;

            if (currentThought.sprite != availableThoughts[0])
            {
                currentThoughtTimer -= Time.fixedDeltaTime;
            }
            else
            {
                thoughtCooldownTimer -= Time.fixedDeltaTime;
            }

            if (dogLoveTimer <= 0)
            {
                dogLoveTimer = maxDogLoveTime;
                SetCurrentThoughtBubble(availableThoughts[1]);
            }

            if (currentThoughtTimer <= 0)
            {
                currentThought.sprite = availableThoughts[0];
            }
        }
    }

    private IEnumerator SlowTick()
    {
        while (true)
        {
            if (target != null) { agent.SetDestination(target.transform.position); }

            CheckForNearbyItems();

            yield return new WaitForSeconds(1);
        }
    }

    private void CheckForNearbyItems()
    {
        float minDistance = 15f;
        List<GameObject> itemsInRange = new List<GameObject>();

        if (thoughtCooldownTimer <= 0)
        {
            for (int i = 0; i < availableMapItems.Count; i++)
            {
                if (availableMapItems[i].activeSelf)
                {
                    if (Vector3.Distance(transform.position, availableMapItems[i].transform.position) < minDistance)
                    {
                        itemsInRange.Add(availableMapItems[i]);
                    }
                }
            }

            //I am 100% sure this can be written better. Unfortunately, I am stupid.
            for (int i = 0; i < itemsInRange.Count; i++)
            {
                if (itemsInRange[i].GetComponent<InventoryItem>().itemType == InventoryManager.Items.dogBone)
                {
                    SetPriorityThought(itemsInRange[i].GetComponent<InventoryItem>().itemType);
                    return;
                }
            }

            for (int i = 0; i < itemsInRange.Count; i++)
            {
                if (itemsInRange[i].GetComponent<InventoryItem>().itemType == InventoryManager.Items.cannedFood)
                {
                    SetPriorityThought(itemsInRange[i].GetComponent<InventoryItem>().itemType);
                    return;
                }
            }

            for (int i = 0; i < itemsInRange.Count; i++)
            {
                if (itemsInRange[i].GetComponent<InventoryItem>().itemType == InventoryManager.Items.water)
                {
                    SetPriorityThought(itemsInRange[i].GetComponent<InventoryItem>().itemType);
                    break;
                }
            }
        }
    }

    private void SetPriorityThought(InventoryManager.Items _item)
    {
        if (currentThought.sprite != availableThoughts[1])
        {
            switch (_item)
            {
                case InventoryManager.Items.dogBone:
                    SetCurrentThoughtBubble(availableThoughts[2]);
                    thoughtCooldownTimer = maxThoughtCooldownTime;
                    break;
                case InventoryManager.Items.cannedFood:
                    SetCurrentThoughtBubble(availableThoughts[3]);
                    thoughtCooldownTimer = maxThoughtCooldownTime;
                    break;
                case InventoryManager.Items.water:
                    SetCurrentThoughtBubble(availableThoughts[4]);
                    thoughtCooldownTimer = maxThoughtCooldownTime;
                    break;
                case InventoryManager.Items.mcDonaldsFries:
                case InventoryManager.Items.newspaperClipping:
                case InventoryManager.Items.Axe:
                case InventoryManager.Items.Shotgun:
                default:
                    break;
            }
        }
    }

    private void SetCurrentThoughtBubble(Sprite _thought)
    {
        currentThought.sprite = _thought;
        currentThoughtTimer = 5;
    }

    public void IncreaseDogLoveLevel()
    {
        if (maxDogLoveTime >= 61.0f)
        {
            maxDogLoveTime -= 15.0f;
        }

        dogLoveTimer = 3.0f;
    }

    public void Die()
    {
        agent.enabled = false;
        anim.SetBool("Dead", true);
        dead = true;
        foreach (GameObject obj in enableOnDeath)
        {
            obj.SetActive(true);
        }
    }

    public void StartNameSequence()
    {
        FindObjectOfType<CamFollow>().NewFollow(gameObject);
        namingTrigger.SetActive(true);
    }
}
