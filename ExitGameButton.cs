using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor; // Needed for EditorApplication
#endif

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        // This will quit the application when built
        Application.Quit();

        // In the editor, stop Play Mode
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif

        Debug.Log("Game closed");
    }
}