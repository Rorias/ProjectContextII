using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public GameObject canvas;
    Dictionary<int, GameObject> NPCMap = new Dictionary<int, GameObject>();

    Dictionary<int, float> layerWeights = new Dictionary<int, float>();

    public List<NPC> disabledNPCs = new List<NPC>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(NPC npc in FindObjectsOfType<NPC>())
        {
            NPCMap.Add(npc.ID, npc.gameObject);
        }
        foreach(NPC npc in disabledNPCs)
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
        if(split.Length == 3)
        {
            prefab.GetComponent<TMPro.TextMeshProUGUI>().text = split[2];
            prefab.GetComponent<DialogTimer>().timer = float.Parse(split[1]);
        }
    }

    public void FreezePlayer()
    {
        FindObjectOfType<UIManager>().FreezeUpdate();
        FindObjectOfType<ThirdPersonMovement>().Freeze();
    }

    public void UnFreezePlayer()
    {
        FindObjectOfType<UIManager>().UnFreezeUpdate();
        FindObjectOfType<ThirdPersonMovement>().UnFreeze();
    }

    public void MakePlayerLayDown()
    {
        FindObjectOfType<ThirdPersonMovement>().anim.SetTrigger("LayDown");
    }

    public void MakePlayerGetUp()
    {
        FindObjectOfType<ThirdPersonMovement>().anim.SetTrigger("GetUp");
    }

    public void KillDog(int npc)
    {
        FindObjectOfType<DogAI>().transform.position = NPCMap[npc].transform.position + new Vector3(0,-1,0);
        FindObjectOfType<UIManager>().doggoHealth.fillAmount = 0;
        FindObjectOfType<DogAI>().Die();
    }

    public void StoreLayerWeights()
    {
        layerWeights = new Dictionary<int, float>();
        Animator anim = FindObjectOfType<ThirdPersonMovement>().anim;
        for (int i = 0; i < anim.layerCount; i++)
        {
            layerWeights.Add(i, anim.GetLayerWeight(i));
            if (i != 0)
                anim.SetLayerWeight(i, 0);
        }
    }

    public void RestoreLayerWeights()
    {
        Animator anim = FindObjectOfType<ThirdPersonMovement>().anim;
        for (int i = 0; i < layerWeights.Keys.Count; i++)
        {
            anim.SetLayerWeight(i, layerWeights[i]);
        }
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
        FindObjectOfType<UIManager>().FreezeUpdate();
        DogAI dog = FindObjectOfType<DogAI>();
        if(dog != null)
        {
            dog.target = FindObjectOfType<ThirdPersonMovement>().gameObject;
            dog.StartNameSequence();
        }
    }
}
