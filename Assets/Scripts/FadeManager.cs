using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FadeManager : MonoBehaviour
{
    private Animator animator;

    protected void Start()
    {
        this.animator = this.GetComponent<Animator>();
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
