using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class InventoryManager : MonoBehaviour
{
    public UIManager uiMng;

    public enum Items { mcDonaldsFries, newspaperClipping, cannedFood, water, Axe, Shotgun, dogBone };

    public GameObject prefabInvItem;
    public Transform itemHolder;

    public GameObject itemOptionMenu;
    public TextMeshProUGUI itemName;
    //These are hard coded because a reference later down needs to be clear
    public Button useButton;
    public Button doggoUseButton;
    public Button inspectButton;
    public Button dropButton;
    public Button cancelButton;

    public Button closeAll;


    public GameObject inspectMenu;

    [HideInInspector] public bool open = false;

    private int activeTab = 0;
    private int maxInvItemCount = 77;

    private InventoryItem selectedItem;

    private List<GameObject> inventoryItems = new List<GameObject>();

    private void Awake()
    {
        itemOptionMenu.SetActive(false);
        inspectMenu.SetActive(false);
    }

    public void AddItemToInventory(InventoryItem _item)
    {
        if (inventoryItems.Count >= maxInvItemCount) { return; }

        GameObject invItem = Instantiate(prefabInvItem, itemHolder);
        invItem.GetComponent<RefInvItem>().refItem = _item;
        invItem.GetComponent<Image>().sprite = _item.inventorySprite;
        inventoryItems.Add(invItem);
    }

    public void RemoveItemFromInventory()
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].GetComponent<RefInvItem>().refItem == selectedItem)
            {
                Destroy(inventoryItems[i].GetComponent<RefInvItem>().refItem.gameObject);
                Destroy(inventoryItems[i]);
                inventoryItems.Remove(inventoryItems[i]);
                break;
            }
        }
    }

    public void UseItemFromInventory()
    {
        bool destroyOnUse = true;
        switch (selectedItem.itemType)
        {
            case Items.mcDonaldsFries:
                uiMng.DecreaseHunger(40);
                break;
            case Items.cannedFood:
                uiMng.DecreaseHunger(180);
                break;
            case Items.water:
                uiMng.DecreaseThirst(120);
                break;
            case Items.Axe:
                FindObjectOfType<GunManager>().EquipAxe();
                EventManager.TriggerEvent("AxeEquip");
                destroyOnUse = false;
                break;
            case Items.Shotgun:
                FindObjectOfType<GunManager>().EquipShotgun();
                destroyOnUse = false;
                break;
            default:
                break;
        }

        if (destroyOnUse) { RemoveItemFromInventory(); }
        CloseItemOptionsMenu();
    }

    public void UseItemOnDoggoFromInventory()
    {
        switch (selectedItem.itemType)
        {
            case Items.mcDonaldsFries:
                uiMng.DecreaseDoggoHunger(20);
                break;
            case Items.cannedFood:
                uiMng.DecreaseDoggoHunger(90);
                break;
            case Items.dogBone:
                uiMng.DecreaseDoggoHunger(180);
                break;
            default:
                break;
        }

        RemoveItemFromInventory();
        CloseItemOptionsMenu();
    }

    public void OpenItemOptionsMenuFor(InventoryItem _item)
    {
        itemOptionMenu.SetActive(true);
        itemName.text = _item.itemName;
        selectedItem = _item;

        closeAll.gameObject.SetActive(true);

        useButton.interactable = _item.useable;
        doggoUseButton.interactable = (FindObjectOfType<DogAI>().dead || uiMng.freezeDogUpdate) ? false : _item.doggoable;
        inspectButton.interactable = _item.inspectable;
    }

    public void CloseItemOptionsMenu()
    {
        itemOptionMenu.SetActive(false);
        selectedItem = null;
        closeAll.gameObject.SetActive(false);
    }

    public void InspectItemInInventory()
    {
        closeAll.gameObject.SetActive(true);
        inspectMenu.SetActive(true);
        inspectMenu.GetComponentInChildren<TextMeshProUGUI>().text = selectedItem.GetComponent<InspectItem>().inspectText;
    }

    public void CloseAllSubMenus()
    {
        closeAll.gameObject.SetActive(false);
        CloseInspectMenu();
        CloseItemOptionsMenu();
    }

    public void CloseInspectMenu()
    {
        inspectMenu.SetActive(false);
        closeAll.gameObject.SetActive(false);
    }

    public void SetActiveInventoryTab(int _tab)
    {

    }

    public void OpenInventory()
    {
        open = true;
        gameObject.SetActive(true);

    }

    public void CloseInventory()
    {
        open = false;
        gameObject.SetActive(false);
    }
}
