using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public enum Items { mcDonaldsFries, newspaperClipping, cannedFood, water };

    public Dictionary<Items, InventoryItemData> inventoryItems = new Dictionary<Items, InventoryItemData>()
    {

    };


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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
