using System.Collections;
using UnityEngine;

public class DefaultAudio : MusicLoader
{
    private AudioSource source;
    private int index = 0;
    private AudioClip clip;

    [ReadOnly] [SerializeField] private Object[] songsList;

    private void Awake()
    {
        if (songsList.Length < 1)
            songsList = Resources.LoadAll("Songs", typeof(AudioClip));
        source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (source.clip != null)
            SendSongPaused(false);
        else
            GetAudio(index);
    }

    private void Update()
    {
        if (songsList.Length <= 0) return;

        if (Input.GetKeyDown(KeyCode.N))
        {
            StopAllCoroutines();
            index = (index + 1) % songsList.Length;
            GetAudio(index);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            StopAllCoroutines();
            if (--index < 0) index = songsList.Length;
            GetAudio(index);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (source.isPlaying)
                source.time = Mathf.Clamp(source.time + 10, 0, source.clip.length - 1);
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            if (source.isPlaying)
                source.time = Mathf.Clamp(source.time - 10, 0, source.clip.length);
        }

        //  set time text
        if (source?.clip ?? false)
            SendSongTime(source.time, source.clip.length);

        //  pause/unpause
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.K))
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
    }

    private void GetAudio(int index)
    {
        if (source == null) return;

        if (songsList.Length > index)
            clip = (AudioClip)songsList[index];
        else
            clip = null;

        if (clip == null) return;

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
    }

    private IEnumerator BufferNextSong()
    {
        //  if still playing song, wait
        while (System.Math.Round(source.time, 2) < System.Math.Round(source.clip.length, 2))
            yield return null;

        //  go to next song
        source.Stop();
        index = (index + 1) % songsList.Length;
        GetAudio(index);
    }
}
