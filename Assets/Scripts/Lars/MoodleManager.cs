using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class MoodleManager : MonoBehaviour
{
    public enum Moodles 
    {
        peckish, hungry, starving, starved, dying, 
        doggoPeckish, doggoHungry, doggoStarving, doggoStarved, doggoDying,
        thirsty, droughty, driedOut, dehydrated, thirstDying };

    public Dictionary<Moodles, MoodleData> moodles = new Dictionary<Moodles, MoodleData>()
    {
        { Moodles.peckish, new MoodleData(){ moodleName = "Peckish", moodleSeverity = new Color(1, 1, 1, 1), moodleDesc = "You could do with some food right about now.",} },
        { Moodles.hungry, new MoodleData(){ moodleName = "Hungry", moodleSeverity = new Color(1, 1, 0.5f, 1), moodleDesc = "Should really find food before you faint.",} },
        { Moodles.starving, new MoodleData(){ moodleName = "Starving", moodleSeverity = new Color(1, 1, 0, 1), moodleDesc = "That dog is starting to look tasty...",} },
        { Moodles.starved, new MoodleData(){ moodleName = "Starved", moodleSeverity =new Color(1, 0.5f, 0, 1), moodleDesc = "EAT SOMETHING. ANYTHING.",} },
        { Moodles.dying, new MoodleData(){ moodleName = "Dying", moodleSeverity = new Color(1, 0, 0, 1), moodleDesc = "Should've eaten when you had the chance.",} },
        { Moodles.doggoPeckish, new MoodleData(){ moodleName = "Peckish", moodleSeverity = new Color(1, 1, 1, 1), moodleDesc = "Your dog seems like he wants a snack.",} },
        { Moodles.doggoHungry, new MoodleData(){ moodleName = "Hungry", moodleSeverity = new Color(1, 1, 0.5f, 1), moodleDesc = "Your dog looks like he wants to eat a lot.",} },
        { Moodles.doggoStarving, new MoodleData(){ moodleName = "Starving", moodleSeverity = new Color(1, 1, 0, 1), moodleDesc = "Your dog is starting to give you suspicious glances.",} },
        { Moodles.doggoStarved, new MoodleData(){ moodleName = "Starved", moodleSeverity =  new Color(1, 0.5f, 0, 1), moodleDesc = "Your dog wants you to know it still loves you.",} },
        { Moodles.doggoDying, new MoodleData(){ moodleName = "Dying", moodleSeverity = new Color(1, 0, 0, 1), moodleDesc = "You're heartless.",} },
        { Moodles.thirsty, new MoodleData(){ moodleName = "Thirsty", moodleSeverity = new Color(1, 1, 1, 1), moodleDesc = "Y",} },
        { Moodles.droughty, new MoodleData(){ moodleName = "Droughty", moodleSeverity = new Color(1, 1, 0.5f, 1), moodleDesc = "Y",} },
        { Moodles.driedOut, new MoodleData(){ moodleName = "Dried Out", moodleSeverity = new Color(1, 1, 0, 1), moodleDesc = "Y",} },
        { Moodles.dehydrated, new MoodleData(){ moodleName = "Dehydrated", moodleSeverity =  new Color(1, 0.5f, 0, 1), moodleDesc = "Y",} },
        { Moodles.thirstDying, new MoodleData(){ moodleName = "Dying", moodleSeverity = new Color(1, 0, 0, 1), moodleDesc = "Y",} },
    };

    private List<GameObject> activeMoodles = new List<GameObject>();

    public GameObject prefabMoodleTM;

    public Transform moodleContent;

    public Sprite hungerMoodleSprite;
    public Sprite doggoHungerMoodleSprite;
    public Sprite thirstMoodleSprite;

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

        moodles[Moodles.thirsty].moodleIcon = thirstMoodleSprite;
        moodles[Moodles.droughty].moodleIcon = thirstMoodleSprite;
        moodles[Moodles.driedOut].moodleIcon = thirstMoodleSprite;
        moodles[Moodles.dehydrated].moodleIcon = thirstMoodleSprite;
        moodles[Moodles.thirstDying].moodleIcon = thirstMoodleSprite;
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
                activeMoodles[i].GetComponent<Image>().color = moodles[_moodle].moodleSeverity;
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
