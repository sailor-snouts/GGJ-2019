using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : FadeManager
{
    public GameObject[] incompleteOnly;
    public GameObject[] completeOnly;

    public void CompleteRoom()
    {
        this.FadeIn();
    }

    public override void FadeInComplete()
    {
        foreach (GameObject obj in this.incompleteOnly)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in this.completeOnly)
        {
            obj.SetActive(true);
        }

        this.FadeOut();
    }
}
