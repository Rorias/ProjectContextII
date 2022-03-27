using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public List<Achievement> achievements;
    public GameObject achievementUIPrefab;
    public GameObject achievementPanel;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Achievement ach in achievements)
        {
            EventManager.StartListening(ach.eventName, ach.eventCallback);
        }
        //ShowAchievements();
    }

    public void ShowAchievements()
    {
        foreach (Achievement ach in achievements)
        {
            GameObject obj = Instantiate(achievementUIPrefab, achievementPanel.transform);
            obj.GetComponent<AchievementUIObject>().achievement = ach;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
