using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipTrigger : MonoBehaviour
{
    public GameObject tooltipPrefab;
    public Transform canvas;
    public GameObject targetPosition;
    GameObject tooltipObj;
    public string tooltipText = "Some Text";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && tooltipObj == null)
        {
            tooltipObj = Instantiate(tooltipPrefab, canvas);
            tooltipObj.GetComponent<Script_Tooltips>().target = targetPosition;
            tooltipObj.GetComponent<Script_Tooltips>().SetText(tooltipText);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && tooltipObj != null)
        {
            Destroy(tooltipObj);
        }
    }
}
