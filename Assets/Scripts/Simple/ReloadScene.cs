using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Reloads current scene
/// </summary>
public class ReloadScene : MonoBehaviour
{
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
