using UnityEngine;

/// <summary>
/// Quit application
/// </summary>
public class QuitGame : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            Quit();
    }

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
