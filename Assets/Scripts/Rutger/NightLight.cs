using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class NightLight : MonoBehaviour
{
    WeatherSystem ws;
    Light lightComp;

    // Start is called before the first frame update
    void Start()
    {
        ws = FindObjectOfType<WeatherSystem>();
        lightComp = GetComponent<Light>();
        StartCoroutine(CheckTime());
    }

    IEnumerator CheckTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (ws.time > 18.5 || ws.time < 6)
                lightComp.enabled = true;
            else
                lightComp.enabled = false;
        }
    }
}
