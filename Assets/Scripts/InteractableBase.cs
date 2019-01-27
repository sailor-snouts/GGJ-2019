using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBase : MonoBehaviour
{
    private RoomManager room;

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
        // @TODO get the minigame?
    }

    virtual public void Interact()
    {
        this.gameWon();
        // @TODO run the minigame passing this to it so the minigame can set it to success or not
    }

    virtual public void gameFailed()
    {

    }

    virtual public void gameWon()
    {
        this.room.CompleteRoom();
    }
}
