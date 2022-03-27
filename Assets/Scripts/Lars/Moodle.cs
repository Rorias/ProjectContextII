using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Moodle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public MoodleManager moodleMng;
    [HideInInspector] public MoodleManager.Moodles moodle;
    [HideInInspector] public GameObject toolTip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        toolTip.SetActive(true);
        moodleMng.SetMoodleTip(moodle);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.SetActive(false);
    }
}
