using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementUIManager : MonoBehaviour
{
    public AchievementManager am;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        ClearUI();
        //am = FindObjectOfType<AchievementManager>();
        am.ShowAchievements();
    }

    void ClearUI()
    {
        for(int i = transform.childCount -1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
