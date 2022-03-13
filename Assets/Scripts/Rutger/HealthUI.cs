using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public int maxHealth = 50;
    public Health healthObject;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = healthObject.GetValue();
        slider.maxValue = maxHealth;
        slider.minValue = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        slider.value = healthObject.GetValue();
    }
}
