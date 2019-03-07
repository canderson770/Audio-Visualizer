using UnityEngine;

/// <summary>
/// Quit application
/// </summary>
public class QuitGame : MonoBehaviour
{
    /// <summary>
    /// Quits application
    /// </summary>
    public void Quit()
    {
        //closes editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

        //closes application
        Application.Quit();
    }
}
