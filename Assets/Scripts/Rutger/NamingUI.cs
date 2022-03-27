using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamingUI : MonoBehaviour
{
    public TMPro.TextMeshProUGUI dogName;
    public GameObject dogUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickName(string name)
    {
        FindObjectOfType<CamFollow>().NewFollow(null);
        FindObjectOfType<UIManager>().UnfreezeDog();
        FindObjectOfType<DialogManager>().UnFreezePlayer();
        dogName.text = name;
        dogUI.SetActive(true);
        gameObject.SetActive(false); 
    }
}
