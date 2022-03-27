using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUIObject : MonoBehaviour
{
    public RawImage icon, done;
    public TextMeshProUGUI achievementName, progress;
    public Achievement achievement;

    Color ndoneCol = new Color(1, 0, 0, 0.3f);
    Color doneCol = new Color(0, 1, 0, 0.3f);

    // Start is called before the first frame update
    void Start()
    {
        icon.texture = achievement.icon;
        achievementName.text = achievement.publicName;
        progress.text = achievement.amount.ToString() + "/" + achievement.needed.ToString();
        if (achievement.isFinished()) done.color = doneCol;
        else done.color = ndoneCol;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        progress.text = achievement.amount.ToString() + "/" + achievement.needed.ToString();
        if (achievement.isFinished()) done.color = doneCol;
        else done.color = ndoneCol;
    }
}
