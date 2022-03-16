using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public enum Items { mcDonaldsFries, newspaperClipping, cannedFood, water };

    public Dictionary<Items, InventoryItemData> possibleItems = new Dictionary<Items, InventoryItemData>()
    {
        { Items.cannedFood, new InventoryItemData(){ currStackCount = 0, maxStackCount = 8, currCount=0 } },
        { Items.mcDonaldsFries, new InventoryItemData(){ currStackCount = 0, maxStackCount = 4, currCount=0 } },
        { Items.newspaperClipping, new InventoryItemData(){ currStackCount = 0, maxStackCount = 1, currCount=0 } },
        { Items.water, new InventoryItemData(){ currStackCount = 0, maxStackCount = 16, currCount=0 } },
    };

    public GameObject prefabInvItem;
    public Transform itemHolder;

    public GameObject itemOptionMenu;

    [HideInInspector] public bool open = false;

    private int activeTab = 0;
    private int maxInvItemCount = 77;

    private List<InventoryItem> inventoryItems = new List<InventoryItem>();

    private void Awake()
    {
        itemOptionMenu.SetActive(false);
    }

    public void AddItemToInventory(InventoryItem _item)
    {
        if (inventoryItems.Count >= maxInvItemCount) { return; }

        foreach (KeyValuePair<Items, InventoryItemData> item in possibleItems)
        {
            if (item.Key == _item.itemType)
            {
                if (item.Value.currStackCount < item.Value.maxStackCount)
                {
                    item.Value.currCount++;
                    item.Value.currStackCount++;
                }
                else
                {
                    item.Value.currCount++;
                }

                Instantiate(prefabInvItem, itemHolder);
                inventoryItems.Add(_item);

                break;
            }
        }
    }

    public void RemoveItemFromInventory(Items _item)
    {

    }

    public void UseItemFromInventory(Items _item)
    {

    }

    public void OpenItemOptionsMenuFor(InventoryItem _item)
    {
        itemOptionMenu.SetActive(true);

        if (!_item.inspectable)
        {
            itemOptionMenu.transform.GetChild(1).GetComponent<Button>().interactable = false;
        }
        else
        {
            itemOptionMenu.transform.GetChild(1).GetComponent<Button>().interactable = true;
        }
    }

    public void CloseItemOptionsMenu()
    {
        itemOptionMenu.SetActive(false);
    }

    public void InspectItemInInventory(Items _item)
    {

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
