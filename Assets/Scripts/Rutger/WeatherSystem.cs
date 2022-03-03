using DigitalRuby.RainMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    public RainScript rs;
    public Light sun;
    public float time = 12.00f;

    public float perl;

    public Vector2 pos = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        pos.x = Random.Range(0, 100f);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * 0.02f;
        if (time > 24) time = 0;

        //use perlin noise to define light intensity and rain
        //Might want to use something else
        pos.x += Time.deltaTime * 0.005f; //0.005f;
        perl = Mathf.PerlinNoise(pos.x, pos.y);
        float darkness = Mathf.Abs(13 - time);
        sun.intensity = 1.5f-perl-(darkness/8);
        rs.RainIntensity = (perl - 0.4f) * 4f;
        rs.WindSoundVolumeModifier = (perl - 0.4f)*2f;
    }
}
