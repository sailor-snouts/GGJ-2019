using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnyKeySceneChange : MonoBehaviour
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
            SceneManager.LoadScene(this.nextScene);
        }
    }
}
