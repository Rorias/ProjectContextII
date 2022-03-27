using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    //Animator anim;
    bool equippedAxe = false;
    bool gotAxe = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FirstNotifications());
        EventManager.StartListening("AxePickup", AxePickup);
        EventManager.StartListening("AxeEquip", AxeEquip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AxePickup()
    {
        if (!gotAxe)
        {
            NotificationManager.ShowNotification("Great! You can equip it using your Inventory, press the 'i' key.");
            gotAxe = true;
        }
        
    }

    public void AxeEquip()
    {
        if (!equippedAxe)
        {
            NotificationManager.ShowNotification("Nice! Now swing it with the left mouse button!");
            equippedAxe = true;
            StartCoroutine(DrinkWater());
        }
    }

    IEnumerator DrinkWater()
    {
        yield return new WaitForSeconds(7);
        NotificationManager.ShowNotification("Make sure to drink and eat to stay alive! Good luck out there! \nYou're gonna need it...");
        yield return new WaitForSeconds(3);
        NotificationManager.ShowNotification("Oh BTW, collect newspapers to find out what happend...");
    }

    IEnumerator FirstNotifications()
    {
        yield return new WaitForSeconds(2);
        NotificationManager.ShowNotification("Hey there! Use WASD to walk and the mouse to look around!");
        yield return new WaitForSeconds(7);
        NotificationManager.ShowNotification("In the top left you can see your health and thrist.\nIn the top right you can see your status.");
        yield return new WaitForSeconds(7);
        NotificationManager.ShowNotification("Pick up that water and that Axe over there.");
    }

    public void GotShotgun()
    {
        NotificationManager.ShowNotification("Hold right mouse button to aim then press left mouse button to shoot.");
    }

    public void Sprinting()
    {
        NotificationManager.ShowNotification("Press and hold shift to sprint!");
    }
}
