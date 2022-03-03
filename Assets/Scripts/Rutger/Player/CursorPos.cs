using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorPos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<RectTransform>().anchoredPosition = Input.mousePosition;
    }
}
