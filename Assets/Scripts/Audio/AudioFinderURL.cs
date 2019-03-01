using System.Collections;
using System.IO;
using UnityEngine;
using SFB;
using System.Linq;

[RequireComponent(typeof(AudioSource))]
public class AudioFinderURL : MusicLoader
{
    private AudioSource source;
    private WWW www;
    private int index = 0;

    [ReadOnly] [SerializeField] private string path;
    [ReadOnly] [SerializeField] private string[] songsList;

    //public enum AudioFileType { OGG, WAV };
    //public AudioFileType fileType = AudioFileType.OGG;

    private void Start()
    {
        source = GetComponent<AudioSource>();

        if (!string.IsNullOrEmpty(path))
            StartSong();
    }

    private void Update()
    {
        if (songsList.Length <= 0) return;

        if (Input.GetKeyDown(KeyCode.N))
        {
            StopAllCoroutines();
            index = (index + 1) % songsList.Length;
            StartCoroutine(GetAudio(index));
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            StopAllCoroutines();
            if (--index < 0) index = songsList.Length;
            StartCoroutine(GetAudio(index));
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (source.isPlaying)
                source.time = Mathf.Clamp(source.time + 10, 0, source.clip.length);
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

    public void ChooseFolder()
    {
        string[] paths = StandaloneFileBrowser.OpenFolderPanel("Select Folder", "", false);

        if (paths.Length == 0) return;

        path = "";
        foreach (var p in paths)
            path += p;
        path += @"\";

        StartSong();
    }

    public void ChooseSongs()
    {
        var extensions = new[] {
                new ExtensionFilter("Sound Files", "ogg", "wav" ),
                new ExtensionFilter("All Files", "*" ),
            };
        songsList = (StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true));

        if (songsList.Length > 0)
            StartCoroutine(GetAudio(index));
    }

    public void StartSong()
    {
        try
        {
            var wavs = Directory.GetFiles(path, "*.WAV");
            var oggs = Directory.GetFiles(path, "*.OGG");
            songsList = wavs.Concat(oggs).ToArray();
        }
        catch { }

        if (songsList.Length > 0)
            StartCoroutine(GetAudio(index));
    }

    private IEnumerator GetAudio(int index)
    {
        if (source == null) yield break;

        if (index >= 0 && index < songsList.Length)
            www = new WWW(songsList[index]);
        yield return www;

        if (www.error != null && www.error != "")
            Debug.Log(www.error);

        source.clip = www.GetAudioClip(false, true);

        if (source.clip == null) yield break;

        if (source.clip.loadState == AudioDataLoadState.Loaded)
        {
            source.time = 0;
            source.Play();
        }

        //  Set song name
        SendSongName(Path.GetFileNameWithoutExtension(songsList[index]));

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
        StartCoroutine(GetAudio(index));
    }
}
