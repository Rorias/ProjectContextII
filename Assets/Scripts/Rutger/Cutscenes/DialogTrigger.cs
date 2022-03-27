using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public string dialog;

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
            FindObjectOfType<DialogManager>().SpawnDialog(dialog);
            Destroy(gameObject);
        }
    }
}
