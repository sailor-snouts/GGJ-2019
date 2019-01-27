using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHomeController : MonoBehaviour
{
    private FadeSceneChange fade;
    private PlayerController player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Player") { return; }

        this.LocateManager();

        this.player = collision.gameObject.GetComponent<PlayerController>();
        this.player.LockMovement();
        fade.FadeIn();
    }

    private void LocateManager()
    {
        foreach (Transform child in this.transform.parent)
        {
            if (child.name == this.name)
            {
                continue;
            }

            if (child.GetComponent<FadeSceneChange>())
            {
                this.fade = child.GetComponent<FadeSceneChange>();
                break;
            }
        }
    }
}
