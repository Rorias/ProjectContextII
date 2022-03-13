using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public GameObject canvas;
    Dictionary<int, GameObject> NPCMap = new Dictionary<int, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(NPC npc in FindObjectsOfType<NPC>())
        {
            NPCMap.Add(npc.ID, npc.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnDialog(string text)
    {
        string[] split = text.Split(':');
        GameObject prefab = Instantiate(Resources.Load<GameObject>("DialogPrefab"), canvas.transform);
        prefab.GetComponent<TMPro.TextMeshProUGUI>().text = split[1];
        if (split[0] == "0")
        {
            prefab.GetComponent<DialogTimer>().follow = FindObjectOfType<ThirdPersonMovement>().transform.GetChild(0).gameObject;
        }
        else
        {
            prefab.GetComponent<DialogTimer>().follow = NPCMap[int.Parse(split[0])];
        }
    }

    public void FreezePlayer()
    {
        FindObjectOfType<ThirdPersonMovement>().Freeze();
    }

    public void UnFreezePlayer()
    {
        FindObjectOfType<ThirdPersonMovement>().UnFreeze();
    }

    public void NPCAgro(int id)
    {
        foreach(NPC npc in FindObjectsOfType<NPC>())
        {
            if(npc.ID == id)
            {
                if(npc.gameObject.GetComponent<NPCAI>() != null)
                {
                    npc.gameObject.GetComponent<NPCAI>().agro = true;
                    npc.gameObject.GetComponent<NPCAI>().target = FindObjectOfType<ThirdPersonMovement>().gameObject;
                }
            }
        }
    }

    public void DogStartFollow()
    {
        DogAI dog = FindObjectOfType<DogAI>();
        if(dog != null)
        {
            dog.target = FindObjectOfType<ThirdPersonMovement>().gameObject;
        }
    }
}
