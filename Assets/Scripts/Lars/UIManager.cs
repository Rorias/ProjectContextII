using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public InventoryManager invMng;

    public MoodleManager moodleMng;
    public MoodleData previewMoodle;

    public Image playerHealth;
    public Image doggoHealth;

    public Health healthHandler;

    //move later? maybe not
    [HideInInspector] public float playerWaterTimer;
    private float playerMaxWaterTime = 180f;

    [HideInInspector] public float playerHungerTimer;
    private float playerMaxHungerTime = 480f;

    [HideInInspector] public float doggoHungerTimer;
    private float doggoMaxHungerTime = 300f;

    private bool paused = false;

    private bool freezeUpdate = false;
    [HideInInspector] public bool freezeDogUpdate { get; private set; } = true;

    private void Start()
    {
        playerWaterTimer = playerMaxWaterTime;
        playerHungerTimer = playerMaxHungerTime;
        doggoHungerTimer = doggoMaxHungerTime;

        invMng.CloseInventory();
        StartCoroutine(ITurnOffDelayed());
    }

    private IEnumerator ITurnOffDelayed()
    {
        yield return new WaitForEndOfFrame();
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (invMng.open)
            {
                invMng.CloseInventory();
            }
            else
            {
                invMng.OpenInventory();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!freezeUpdate)
        {
            if (playerWaterTimer > -1)
            {
                playerWaterTimer -= Time.fixedDeltaTime;
            }

            if (playerHungerTimer > -1)
            {
                playerHungerTimer -= Time.fixedDeltaTime;
            }

            if (doggoHungerTimer > -1 && !freezeDogUpdate)
            {
                doggoHungerTimer -= Time.fixedDeltaTime;
            }

            UpdateUI();
        }
    }

    private void Unpause()
    {
        paused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        Cursor.visible = false;
    }

    private void Pause()
    {
        paused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;
    }

    public void FreezeUpdate()
    {
        freezeUpdate = true;
    }

    public void UnFreezeUpdate()
    {
        freezeUpdate = false;
    }

    public void UnfreezeDog()
    {
        freezeDogUpdate = false;
    }

    public void DecreaseHunger(int _amount)
    {
        playerHungerTimer += _amount;
        healthHandler.Heal(_amount / 4);
    }

    public void DecreaseDoggoHunger(int _amount)
    {
        doggoHungerTimer += _amount;
    }

    public void DecreaseThirst(int _amount)
    {
        playerWaterTimer += _amount;
    }

    public void UpdateUI()
    {
        playerHealth.fillAmount = (float)healthHandler.GetValue() / healthHandler.maxHealth;

        if (playerHungerTimer <= 0 || playerWaterTimer <= 0)
        {
            healthHandler.Damage(0.003f);
        }

        if (doggoHungerTimer <= 0)
        {
            doggoHealth.fillAmount -= 0.001f;
            if(doggoHealth.fillAmount <= 0)
            {
                Debug.Log("Doggo Dead!");
                FindObjectOfType<DogAI>().Die();
            }
        }

        if (playerHungerTimer < 0)
        {
            moodleMng.SetMoodle(MoodleManager.Moodles.dying);
        }
        else if (playerHungerTimer < 80)
        {
            moodleMng.SetMoodle(MoodleManager.Moodles.starved);
        }
        else if (playerHungerTimer < 180)
        {
            moodleMng.SetMoodle(MoodleManager.Moodles.starving);
        }
        else if (playerHungerTimer < 280)
        {
            moodleMng.SetMoodle(MoodleManager.Moodles.hungry);
        }
        else if (playerHungerTimer < 380)
        {
            moodleMng.CreateMoodle(MoodleManager.Moodles.peckish);
        }
        else
        {
            moodleMng.RemoveMoodle(MoodleManager.Moodles.peckish);
        }

        if (playerWaterTimer < 0)
        {
            moodleMng.SetMoodle(MoodleManager.Moodles.thirstDying);
        }
        else if (playerWaterTimer < 40)
        {
            moodleMng.SetMoodle(MoodleManager.Moodles.dehydrated);
        }
        else if (playerWaterTimer < 80)
        {
            moodleMng.SetMoodle(MoodleManager.Moodles.driedOut);
        }
        else if (playerWaterTimer < 120)
        {
            moodleMng.SetMoodle(MoodleManager.Moodles.droughty);
        }
        else if (playerWaterTimer < 160)
        {
            moodleMng.CreateMoodle(MoodleManager.Moodles.thirsty);
        }
        else
        {
            moodleMng.RemoveMoodle(MoodleManager.Moodles.thirsty);
        }

        if (doggoHungerTimer < 0)
        {
            moodleMng.SetDogMoodle(MoodleManager.Moodles.doggoDying);
        }
        else if (doggoHungerTimer < 60)
        {
            moodleMng.SetDogMoodle(MoodleManager.Moodles.doggoStarved);
        }
        else if (doggoHungerTimer < 120)
        {
            moodleMng.SetDogMoodle(MoodleManager.Moodles.doggoStarving);
        }
        else if (doggoHungerTimer < 180)
        {
            moodleMng.SetDogMoodle(MoodleManager.Moodles.doggoHungry);
        }
        else if (doggoHungerTimer < 240)
        {
            moodleMng.CreateDogMoodle(MoodleManager.Moodles.doggoPeckish);
        }
        else
        {
            moodleMng.RemoveDogMoodle(MoodleManager.Moodles.doggoPeckish);
        }
    }
}
