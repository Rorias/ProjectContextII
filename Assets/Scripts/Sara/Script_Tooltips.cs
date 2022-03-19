using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Script_Tooltips : MonoBehaviour
{

    public BoxCollider collider;
   public GameObject tooltip;
    
    // Start is called before the first frame update
    void Start()
    {
        tooltip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col) { 
tooltip.SetActive(true);
    }

    void OnTriggerExit() { 
  tooltip.SetActive(false);
    }
}
