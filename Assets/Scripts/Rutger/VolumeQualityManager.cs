using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeQualityManager : MonoBehaviour
{
    public VolumeProfile[] profiles;
    int lastQualityLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Volume>().profile = profiles[QualitySettings.GetQualityLevel()];
    }

    // Update is called once per frame
    void Update()
    {
        if(QualitySettings.GetQualityLevel() != lastQualityLevel)
        {
            lastQualityLevel = QualitySettings.GetQualityLevel();
            GetComponent<Volume>().profile = profiles[lastQualityLevel];
        }
    }
}
