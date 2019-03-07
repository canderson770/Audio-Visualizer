using System.Collections;
using System.IO;
using UnityEngine;
using SFB;
using System.Linq;

[RequireComponent(typeof(AudioSource))]
public class AudioFinderURL : MusicPlayer
{
    private WWW www;

    [ReadOnly] [SerializeField] private string path;
    [ReadOnly] [SerializeField] private string[] songsList;

    private void Start()
    {
        songAmount = songsList.Length;

        if (!string.IsNullOrEmpty(path))
            StartSong();
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
        var extensions = new[]
        {
            new ExtensionFilter("Sound Files", "ogg", "wav" ),
            new ExtensionFilter("All Files", "*" ),
        };

        songsList = (StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true));
        songAmount = songsList.Length;

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

        StartCoroutine(GetAudio(index));
    }

    protected override IEnumerator GetAudio(int index)
    {
        if (source == null || songsList.Length <= 0) yield break;

        if (index >= 0 && index < songAmount)
            www = new WWW(songsList[index]);
        yield return www;

        if (!string.IsNullOrEmpty(www?.error))
            Debug.Log(www.error);

        source.clip = www.GetAudioClip(false, true);

        if (source.clip == null) yield break;

        if (source.clip.loadState == AudioDataLoadState.Loaded)
        {
            source.time = 0;
            source.Play();
        }

        //  Set song name
        if (index >= 0 && index < songAmount)
            SendSongName(Path.GetFileNameWithoutExtension(songsList[index]));

        //  Start particles
        SendSongPaused(false);

        //  Check for next song
        StartCoroutine(BufferNextSong());
    }
}
