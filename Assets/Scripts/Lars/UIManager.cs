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
    public Image playerWater;
    public Image doggoHealth;

    public Health healthHandler;

    //move later? maybe not
    [HideInInspector] public float playerWaterTimer = 120f;
    private float playerMaxWaterTime = 120f;

    [HideInInspector] public float playerHungerTimer = 300f;
    private float playerMaxHungerTime = 300f;

    [HideInInspector] public float doggoHungerTimer = 180f;
    private float doggoMaxHungerTime = 180f;

    private bool paused = false;
    private bool hungerUpdated = false;

    private float lostHealth = 0.0f;

    bool dogDead = false;

    private void Start()
    {
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
        if (playerWaterTimer > -1)
        {
            playerWaterTimer -= Time.fixedDeltaTime;
        }

        if (playerHungerTimer > -1)
        {
            playerHungerTimer -= Time.fixedDeltaTime;
        }

        if (doggoHungerTimer > -1)
        {
            doggoHungerTimer -= Time.fixedDeltaTime;
        }

        UpdateUI();
    }

    private void Unpause()
    {
        paused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    private void Pause()
    {
        paused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void DecreaseHunger(int _amount)
    {
        playerHungerTimer += _amount;
        healthHandler.Heal(_amount / 2);
        //lostHealth = 0;
    }

    public void DecreaseDoggoHunger(int _amount)
    {
        doggoHungerTimer += _amount;
    }

    public void DecreaseThirst(int _amount)
    {
        playerWaterTimer += _amount;
        //lostHealth = 0;
    }

    public void UpdateUI()
    {
        playerWater.fillAmount = playerWaterTimer / playerMaxWaterTime;
        playerHealth.fillAmount = ((float)healthHandler.GetValue() / healthHandler.maxHealth);// - lostHealth;

        if (playerHungerTimer <= 0 || playerWaterTimer <= 0)
        {
            //lostHealth += 0.001f;
            healthHandler.Damage(0.003f);
        }

        if (doggoHungerTimer <= 0)
        {
            doggoHealth.fillAmount -= 0.001f;
            if(doggoHealth.fillAmount <= 0)
            {
                Debug.Log("Dogo Dead!");
                FindObjectOfType<DogAI>().Die();
            }
        }

        if (playerHungerTimer < 0)
        {
            moodleMng.SetMoodle(MoodleManager.Moodles.dying);
        }
        else if (playerHungerTimer < 60)
        {
            moodleMng.SetMoodle(MoodleManager.Moodles.starved);
        }
        else if (playerHungerTimer < 120)
        {
            moodleMng.SetMoodle(MoodleManager.Moodles.starving);
        }
        else if (playerHungerTimer < 180)
        {
            moodleMng.SetMoodle(MoodleManager.Moodles.hungry);
        }
        else if (playerHungerTimer < 240)
        {
            moodleMng.CreateMoodle(MoodleManager.Moodles.peckish);
        }
        else
        {
            moodleMng.RemoveMoodle(MoodleManager.Moodles.peckish);
        }

        if (doggoHungerTimer < 0)
        {
            moodleMng.SetMoodle(MoodleManager.Moodles.doggoDying);
        }
        else if (doggoHungerTimer < 60)
        {
            moodleMng.SetMoodle(MoodleManager.Moodles.doggoStarved);
        }
        else if (doggoHungerTimer < 90)
        {
            moodleMng.SetMoodle(MoodleManager.Moodles.doggoStarving);
        }
        else if (doggoHungerTimer < 120)
        {
            moodleMng.SetMoodle(MoodleManager.Moodles.doggoHungry);
        }
        else if (doggoHungerTimer < 150)
        {
            moodleMng.CreateMoodle(MoodleManager.Moodles.doggoPeckish);
        }
        else
        {
            moodleMng.RemoveMoodle(MoodleManager.Moodles.doggoPeckish);
        }
    }
}
