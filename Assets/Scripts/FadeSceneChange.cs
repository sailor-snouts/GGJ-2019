using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeSceneChange : FadeManager
{
    public string nextScene;
    public bool canQuitOnEscape = true;

    override public void FadeInComplete()
    {
        SceneManager.LoadScene(this.nextScene);
    }
}
