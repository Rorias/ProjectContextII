using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Script_Tooltips : MonoBehaviour
{
    public GameObject target;
    public TMPro.TextMeshProUGUI textObj;

    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Camera camera in FindObjectsOfType<Camera>())
        {
            if (camera.CompareTag("MainCamera")) cam = camera;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().position = cam.WorldToScreenPoint(target.transform.position);
    }

    public void SetText(string text)
    {
        textObj.text = text;
    }
}
