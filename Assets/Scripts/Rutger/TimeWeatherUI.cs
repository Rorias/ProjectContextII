using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeWeatherUI : MonoBehaviour
{
    public TextMeshProUGUI timeDisp, weatherDisp;
    public WeatherSystem ws;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(updateUI());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    IEnumerator updateUI()
    {
        for (; ; )
        {
            System.TimeSpan ts = System.TimeSpan.FromHours(ws.time);
            timeDisp.text = ts.ToString("hh\\:mm");
            weatherDisp.text = "Clear";
            if (ws.perl > .3f) weatherDisp.text = "Overcast";
            if (ws.perl > .4f) weatherDisp.text = "Light Rain";
            if (ws.perl > .48f) weatherDisp.text = "Rain";
            if (ws.perl > .55f) weatherDisp.text = "Downpour";
            if (ws.perl > .6f) weatherDisp.text = "Storm";
            yield return new WaitForSeconds(2f);
        }
    }
}
