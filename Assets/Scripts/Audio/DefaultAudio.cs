using System.Collections;
using System.Linq;
using UnityEngine;

public class DefaultAudio : MusicPlayer
{
    private AudioClip clip;
    [ReadOnly] [SerializeField] private AudioClip[] songsList;

    protected override void Awake()
    {
        base.Awake();

        if (songsList.Length < 1)
            songsList = Resources.LoadAll("Songs", typeof(AudioClip)).OfType<AudioClip>().ToArray();

        songAmount = songsList.Length;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (source.clip != null)
            SendSongPaused(false);
        else
            StartCoroutine(GetAudio(index));
    }

    protected override IEnumerator GetAudio(int index)
    {
        if (source == null) yield break;

        if (songsList.Length > index)
            clip = songsList[index];
        else
            clip = null;

        if (clip == null) yield break;

        //  set clip
        source.clip = clip;
        source.time = 0;

        //  play song
        source.Play();

        //  Set song name
        SendSongName(clip.name);

        //  Start particles
        SendSongPaused(false);

        //  Check for next song
        StartCoroutine(BufferNextSong());

        yield return null;
    }
}
