using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class MoodleManager : MonoBehaviour
{
    public enum Moodles { peckish, hungry, starving, starved, dying, doggoPeckish, doggoHungry, doggoStarving, doggoStarved, doggoDying, };

    public Dictionary<Moodles, MoodleData> moodles = new Dictionary<Moodles, MoodleData>()
    {
        { Moodles.peckish, new MoodleData(){ moodleName = "Peckish", moodleSeverity = 0.2f, moodleDesc = "You could do with some food right about now.",} },
        { Moodles.hungry, new MoodleData(){ moodleName = "Hungry", moodleSeverity = 0.4f, moodleDesc = "Should really find food before you faint.",} },
        { Moodles.starving, new MoodleData(){ moodleName = "Starving", moodleSeverity = 0.6f, moodleDesc = "That dog is starting to look tasty...",} },
        { Moodles.starved, new MoodleData(){ moodleName = "Starved", moodleSeverity = 0.8f, moodleDesc = "EAT SOMETHING. ANYTHING.",} },
        { Moodles.dying, new MoodleData(){ moodleName = "Dying", moodleSeverity = 1.0f, moodleDesc = "Should've eaten when you had the chance.",} },
        { Moodles.doggoPeckish, new MoodleData(){ moodleName = "Peckish", moodleSeverity = 0.2f, moodleDesc = "Your dog seems like he wants a snack.",} },
        { Moodles.doggoHungry, new MoodleData(){ moodleName = "Hungry", moodleSeverity = 0.4f, moodleDesc = "Your dog looks like he wants to eat a lot.",} },
        { Moodles.doggoStarving, new MoodleData(){ moodleName = "Starving", moodleSeverity = 0.6f, moodleDesc = "Your dog is starting to give you suspicious glances.",} },
        { Moodles.doggoStarved, new MoodleData(){ moodleName = "Starved", moodleSeverity = 0.8f, moodleDesc = "Your dog wants you to know it still loves you.",} },
        { Moodles.doggoDying, new MoodleData(){ moodleName = "Dying", moodleSeverity = 1.0f, moodleDesc = "You're heartless.",} },
    };

    private List<GameObject> activeMoodles = new List<GameObject>();

    public GameObject prefabMoodleTM;

    public Transform moodleContent;

    public Sprite hungerMoodleSprite;
    public Sprite doggoHungerMoodleSprite;

    private void Awake()
    {
        InitializeMoodleSprites();
    }

    private void InitializeMoodleSprites()
    {
        moodles[Moodles.peckish].moodleIcon = hungerMoodleSprite;
        moodles[Moodles.hungry].moodleIcon = hungerMoodleSprite;
        moodles[Moodles.starving].moodleIcon = hungerMoodleSprite;
        moodles[Moodles.starved].moodleIcon = hungerMoodleSprite;
        moodles[Moodles.dying].moodleIcon = hungerMoodleSprite;
        moodles[Moodles.doggoPeckish].moodleIcon = doggoHungerMoodleSprite;
        moodles[Moodles.doggoHungry].moodleIcon = doggoHungerMoodleSprite;
        moodles[Moodles.doggoStarving].moodleIcon = doggoHungerMoodleSprite;
        moodles[Moodles.doggoStarved].moodleIcon = doggoHungerMoodleSprite;
        moodles[Moodles.doggoDying].moodleIcon = doggoHungerMoodleSprite;
    }

    public void CreateMoodle(Moodles _moodle)
    {
        for (int i = 0; i < activeMoodles.Count; i++)
        {
            if (activeMoodles[i].GetComponent<Image>().sprite == moodles[_moodle].moodleIcon)
            {
                return;
            }
        }

        GameObject moodle = Instantiate(prefabMoodleTM, moodleContent);
        moodle.GetComponent<Image>().sprite = moodles[_moodle].moodleIcon;
        activeMoodles.Add(moodle);
        SetMoodle(_moodle);
    }

    public void SetMoodle(Moodles _moodle)
    {
        for (int i = 0; i < activeMoodles.Count; i++)
        {
            if (activeMoodles[i].GetComponent<Image>().sprite == moodles[_moodle].moodleIcon)
            {
                activeMoodles[i].GetComponent<Image>().color = new Color(1, 1 - moodles[_moodle].moodleSeverity, 1 - moodles[_moodle].moodleSeverity, 1);
                activeMoodles[i].GetComponent<Moodle>().title.GetComponent<TextMeshProUGUI>().text = moodles[_moodle].moodleName;
                activeMoodles[i].GetComponent<Moodle>().desc.GetComponent<TextMeshProUGUI>().text = moodles[_moodle].moodleDesc;
            }
        }
    }

    public void RemoveMoodle(Moodles _moodle)
    {
        for (int i = 0; i < activeMoodles.Count; i++)
        {
            if (activeMoodles[i].GetComponent<Image>().sprite == moodles[_moodle].moodleIcon)
            {
                Destroy(activeMoodles[i]);
                activeMoodles.RemoveAt(i);
                break;
            }
        }
    }
}
