using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultAudio : MusicLoader
{
    //private int index = 0;
    //[ReadOnly] [SerializeField] private Object[] songsList;

    //private void Awake()
    //{
    //    songsList = Resources.LoadAll("Songs", typeof(AudioClip));
    //}

    //private void OnEnable()
    //{
    //    SendSongPaused(false);
    //}

    //private void Update()
    //{
    //if (songsList.Length <= 0) return;

    //if (Input.GetKeyDown(KeyCode.N))
    //{
    //    StopAllCoroutines();
    //    index = (index + 1) % songsList.Length;
    //    StartCoroutine(GetAudio(index));
    //}

    //if (Input.GetKeyDown(KeyCode.L))
    //{
    //    if (source.isPlaying)
    //        source.time = Mathf.Clamp(source.time + 10, 0, source.clip.length);
    //}
    //else if (Input.GetKeyDown(KeyCode.J))
    //{
    //    if (source.isPlaying)
    //        source.time = Mathf.Clamp(source.time - 10, 0, source.clip.length);
    //}

    ////  set time text
    //if (source?.clip ?? false)
    //    SendSongTime(source.time, source.clip.length);

    ////  pause/unpause
    //if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.K))
    //{
    //    if (source.isPlaying)
    //    {
    //        source.Pause();
    //        SendSongPaused(true);
    //    }
    //    else
    //    {
    //        source.UnPause();
    //        SendSongPaused(false);
    //    }
    //}
    //}
}
