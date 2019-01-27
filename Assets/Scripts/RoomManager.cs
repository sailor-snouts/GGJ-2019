using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : FadeManager
{
    public GameObject[] incompleteOnly;
    public GameObject[] completeOnly;
    private PlayerController player;
    private bool isCompleted = false;
    private float timeUntilFadeOut = 2f;
    private float timeUntilFlip = 2f;

    new private void Start()
    {
        base.Start();
    }

    public void CompleteRoom()
    {
        this.player = FindObjectOfType<PlayerController>();
        this.player.LockMovement();
        this.FadeIn();
    }

    private void Flip()
    {
        foreach (GameObject obj in this.incompleteOnly)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in this.completeOnly)
        {
            obj.SetActive(true);
        }

    }

    void Update()
    {
        if(!this.isCompleted) { return; }
        if(this.timeUntilFlip > 0)
        {
            this.timeUntilFlip -= Time.deltaTime;
            if (this.timeUntilFlip <= 0)
            {
                this.Flip();
            }
        }
        else if (this.timeUntilFadeOut > 0)
        {
            this.timeUntilFadeOut -= Time.deltaTime;
            if (this.timeUntilFadeOut <= 0)
            {
                this.FadeOut();
            }
        }
    }

    public override void FadeInComplete()
    {
        this.isCompleted = true;
        this.timeUntilFadeOut = 1f;
        this.timeUntilFlip = 1f;
    }

    public override void FadeOutComplete()
    {
        this.player.UnlockMovement();
    }
}
