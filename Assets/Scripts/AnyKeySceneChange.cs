using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnyKeySceneChange : FadeManager
{
    public string nextScene;
    public bool canQuitOnEscape = true;

    void Update()
    {
        if(this.canQuitOnEscape && Input.GetKey(KeyCode.Escape))
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
        else if(Input.anyKey)
        {
            this.FadeIn();
        }
    }

    override public void FadeInComplete()
    {
        SceneManager.LoadScene(this.nextScene);
    }

    override public void FadeOutComplete()
    {
        Debug.Log("overide is working");
    }
}
