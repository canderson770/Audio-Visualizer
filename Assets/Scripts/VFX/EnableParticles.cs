using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableParticles : MonoBehaviour
{
    private ParticleSystem ps;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        MusicPlayer.SongPaused += Enable;
    }
    private void OnDisable()
    {
        MusicPlayer.SongPaused -= Enable;
    }

    private void Enable(bool isPaused)
    {
        if (ps == null) return;

        if (isPaused)
            ps.Pause();
        else
            ps.Play();
    }
}
