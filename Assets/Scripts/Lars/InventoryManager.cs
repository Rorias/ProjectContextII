using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public enum Items { mcDonaldsFries, newspaperClipping, cannedFood, water };

    public Dictionary<Items, InventoryItemData> inventoryItems = new Dictionary<Items, InventoryItemData>()
    {
        { Items.cannedFood, new InventoryItemData(){ currStackCount = 0, maxStackCount = 1 } }
    };

    private int activeTab = 0;
    private int maxInvItemCount = 77;


    public void AddItemToInventory(Items _item)
    {
        foreach (KeyValuePair<Items, InventoryItemData> item in inventoryItems)
        {
            if (item.Key == _item)
            {
                if (item.Value.currStackCount < item.Value.maxStackCount)
                {
                    item.Value.currStackCount++;
                }
            }
        }
    }

    public void RemoveItemFromInventory(Items _item)
    {

    }

    public void UseItemFromInventory(Items _item)
    {

    }

    public void OpenItemOptionsMenuFor(Items _item)
    {

    }

    public void InspectItemInInventory(Items _item)
    {

    }

    public void SetActiveInventoryTab(int _tab)
    {

    }

    public void OpenInventory()
    {

    }

    public void CloseInventory()
    {

    }
}
