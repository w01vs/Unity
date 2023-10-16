using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private GameObject door;
    private bool doorOpen;

    void Start()
    {
        promptMessage = doorOpen ? "Close Door" : "Open Door";
    }

    protected override void Interact()
    {
        doorOpen = !doorOpen;
        door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
        promptMessage = doorOpen ? "Close Door" : "Open Door";
    }
}
