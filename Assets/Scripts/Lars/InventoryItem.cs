using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class InventoryItem : MonoBehaviour
{
    private InventoryManager im;

    public InventoryManager.Items itemType;
    public Sprite inventorySprite;
    public string itemName;
    public int activeTab;
    public bool useable;
    public bool doggoable;
    public bool inspectable;

    private TextMeshProUGUI text;
    public string pickupText;

    private bool inRange = false;

    public string AchievementEventCall = "";

    private void Awake()
    {
        im = GameObject.Find("Inventory").GetComponent<InventoryManager>();
        text = GameObject.Find("InventoryItemToolText").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            im.AddItemToInventory(this);
            inRange = false;
            text.text = string.Empty;
            if(AchievementEventCall != "")
            {
                //EventManager.TriggerEvent(AchievementEventCall);
            }
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider _coll)
    {
        if (_coll.CompareTag("Player"))
        {
            inRange = true;
            text.text = pickupText;
        }
    }

    private void OnTriggerExit(Collider _coll)
    {
        if (_coll.CompareTag("Player"))
        {
            inRange = false;
            text.text = string.Empty;
        }
    }
}
