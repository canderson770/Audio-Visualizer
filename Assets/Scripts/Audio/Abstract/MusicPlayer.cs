using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class MusicPlayer : MonoBehaviour
{
    private WaitForSeconds wait;
    protected AudioSource source;
    protected int index = 0, songAmount;

    public float delayBetweenSongs = .5f;

    public static UnityAction<bool> SongPaused;
    public static UnityAction<string> SongName;
    public static UnityAction<float, float> SongTime;

    protected void SendSongPaused(bool isPaused)
    {
        SongPaused?.Invoke(isPaused);
    }

    protected void SendSongName(string name)
    {
        SongName?.Invoke(name);
    }

    protected void SendSongTime(float currentTime, float maxTime)
    {
        SongTime?.Invoke(currentTime, maxTime);
    }

    protected virtual void Awake()
    {
        wait = new WaitForSeconds(delayBetweenSongs);
        source = GetComponent<AudioSource>();

        if (source.clip != null)
            SendSongPaused(false);
        else
            StartCoroutine(GetAudio(index));
    }

    protected virtual void OnEnable()
    {
        MusicControls.PlayPause += PlayOrPause;
        MusicControls.Skip += Next;
        MusicControls.FastForward += FastForward;
    }
    protected virtual void OnDisable()
    {
        MusicControls.PlayPause -= PlayOrPause;
        MusicControls.Skip -= Next;
        MusicControls.FastForward -= FastForward;
    }

    /// <summary>
    /// Goes to next or previous song
    /// </summary>
    protected virtual void Next(bool reverse = false)
    {
        StopAllCoroutines();

        if (reverse == false)
        {
            index = (index + 1) % songAmount;
        }
        else
        {
            index--;
            if (index < 0)
                index = songAmount;
        }

        index = Mathf.Clamp(index, 0, songAmount);
        StartCoroutine(GetAudio(index));
    }

    /// <summary>
    /// Skips forward or back a certain amount of seconds
    /// </summary>
    protected virtual void FastForward(float time)
    {
        source.time = Mathf.Clamp(source.time + time, 0, source.clip.length - 1);
    }

    /// <summary>
    /// Toggles between play and pause
    /// </summary>
    protected virtual void PlayOrPause()
    {
        if (source.isPlaying)
        {
            source.Pause();
            SendSongPaused(true);
        }
        else
        {
            source.UnPause();
            SendSongPaused(false);
        }
    }

    protected virtual void Update()
    {
        if (songAmount <= 0) return;

        //  update time text
        if (source?.clip ?? false) SendSongTime(source.time, source.clip.length);
    }

    /// <summary>
    /// Finds next song
    /// </summary>
    protected virtual IEnumerator GetAudio(int index) { yield break; }

    /// <summary>
    /// Starts next song when song ends
    /// </summary>
    protected IEnumerator BufferNextSong()
    {
        //  if still playing song, wait
        while (System.Math.Round(source.time, 2) < System.Math.Round(source.clip.length, 2))
            yield return null;

        //  delay
        yield return wait;

        //  go to next song
        source.Stop();
        index = (index + 1) % songAmount;
        StartCoroutine(GetAudio(index));
    }
}
