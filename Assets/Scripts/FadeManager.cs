using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FadeManager : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        this.animator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            this.FadeIn();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            this.FadeOut();
        }
    }

    public void FadeIn()
    {
        this.animator.ResetTrigger("FadeOut");
        this.animator.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        this.animator.ResetTrigger("FadeIn");
        this.animator.SetTrigger("FadeOut");
    }

    public virtual void FadeInComplete()
    {

    }

    public virtual void FadeOutComplete()
    {

    }
}
