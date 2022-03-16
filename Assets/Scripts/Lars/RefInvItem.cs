using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RefInvItem : MonoBehaviour
{
    public InventoryItem refItem;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(()=> GameObject.Find("Inventory").GetComponent<InventoryManager>().OpenItemOptionsMenuFor(refItem));
    }
}
