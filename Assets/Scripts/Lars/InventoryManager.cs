using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class InventoryManager : MonoBehaviour
{
    public UIManager uiMng;

    public enum Items { mcDonaldsFries, newspaperClipping, cannedFood, water };

    public GameObject prefabInvItem;
    public Transform itemHolder;

    public GameObject itemOptionMenu;
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
        switch (selectedItem.itemType)
        {
            case Items.mcDonaldsFries:
                uiMng.DecreaseHunger(20);
                break;
            case Items.cannedFood:
                uiMng.DecreaseHunger(90);
                break;
            case Items.water:
                uiMng.DecreaseThirst(120);
                break;
            default:
                break;
        }

        RemoveItemFromInventory();
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
            default:
                break;
        }

        RemoveItemFromInventory();
        CloseItemOptionsMenu();
    }

    public void OpenItemOptionsMenuFor(InventoryItem _item)
    {
        itemOptionMenu.SetActive(true);
        selectedItem = _item;

        if (!_item.doggoable)
        {
            itemOptionMenu.transform.GetChild(1).GetComponent<Button>().interactable = false;
        }
        else
        {
            itemOptionMenu.transform.GetChild(1).GetComponent<Button>().interactable = true;
        }

        if (!_item.inspectable)
        {
            itemOptionMenu.transform.GetChild(2).GetComponent<Button>().interactable = false;
        }
        else
        {
            itemOptionMenu.transform.GetChild(2).GetComponent<Button>().interactable = true;
        }
    }

    public void CloseItemOptionsMenu()
    {
        itemOptionMenu.SetActive(false);
        selectedItem = null;
    }

    public void InspectItemInInventory()
    {
        inspectMenu.SetActive(true);
        inspectMenu.GetComponentInChildren<TextMeshProUGUI>().text = selectedItem.GetComponent<InspectItem>().inspectText;
    }

    public void CloseInspectMenu()
    {
        inspectMenu.SetActive(false);
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
