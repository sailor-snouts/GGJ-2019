using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBase : MonoBehaviour
{
    private RoomManager room;
    private PlayerController player;
    public GameObject actionBar;

    void Start()
    {
        foreach (Transform child in this.transform.parent)
        {
            if (child.name == this.name)
            {
                continue;
            }

            if (child.GetComponent<RoomManager>())
            {
                this.room = child.GetComponent<RoomManager>();
                break;
            }
        }

        this.player = FindObjectOfType<PlayerController>();
    }

    virtual public void Interact()
    {
        this.player.LockMovement();
        this.actionBar.SetActive(true);
        ActionBarController actionBarController = this.actionBar.GetComponent<ActionBarController>();
        actionBarController.interactable = this;
    }

    virtual public void gameFailed()
    {
    }

    virtual public void gameWon()
    {
        this.room.CompleteRoom();
        this.actionBar.SetActive(false);
    }
}
