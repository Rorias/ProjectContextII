using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Moodle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject title;
    public GameObject desc;

    private void Awake()
    {
        title.SetActive(false);
        desc.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        title.SetActive(true);
        desc.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        title.SetActive(false);
        desc.SetActive(false);
    }
}
