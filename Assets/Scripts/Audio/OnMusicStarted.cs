using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnMusicStarted : MonoBehaviour
{
    public UnityEvent onMusicStarted;

    private void OnEnable()
    {
        MusicLoader.SongPaused += OnStarted;
    }
    private void OnDisable()
    {
        MusicLoader.SongPaused -= OnStarted;
    }

    private void OnStarted(bool paused)
    {
        if (!paused)
            onMusicStarted.Invoke();
    }
}
