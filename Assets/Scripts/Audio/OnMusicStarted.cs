using UnityEngine;
using UnityEngine.Events;

public class OnMusicStarted : MonoBehaviour
{
    public UnityEvent onMusicStarted;

    private void OnEnable()
    {
        MusicPlayer.SongPaused += OnStarted;
    }
    private void OnDisable()
    {
        MusicPlayer.SongPaused -= OnStarted;
    }

    private void OnStarted(bool paused)
    {
        if (!paused)
            onMusicStarted.Invoke();
    }
}
