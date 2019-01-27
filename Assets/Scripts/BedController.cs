using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedController : InteractableBase
{
    public override void Interact()
    {
        FindObjectOfType<FadeSceneChange>().FadeIn();
    }
}
