using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryControllerInputManager : MonoBehaviour
{
    EventSystem eventSystem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(eventSystem.currentSelectedGameObject == null)
        {
            ResetSelected();
        }
    }

    public void ResetSelected()
    {
        if (transform.childCount > 0)
        {
            eventSystem.SetSelectedGameObject(null);
            eventSystem.SetSelectedGameObject(transform.GetChild(0).gameObject);
        }
    }
}
