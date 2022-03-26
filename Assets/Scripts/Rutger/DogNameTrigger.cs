using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogNameTrigger : MonoBehaviour
{
    public GameObject namingUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ThirdPersonMovement>())
        {
            namingUI.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
