using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SFB;

[RequireComponent(typeof(AudioSource))]
public class AudioFinderURL : MonoBehaviour
{
    private AudioSource source;
    private WWW www;
    private int index = 0;

    public string path;
    public string[] filePaths;
    public enum AudioFileType { OGG, MP3, WAV };
    public AudioFileType fileType = AudioFileType.OGG;
    public Text songText;
    public Text timeText;
    public ParticleSystem ps;



    private void Start()
    {
        source = GetComponent<AudioSource>();

        if (!string.IsNullOrEmpty(path))
            StartSong();
    }

    private void Update()
    {
        if (filePaths.Length <= 0)
            return;


        if (Input.GetKeyDown(KeyCode.N))
        {
            StopAllCoroutines();
            index = (index + 1) % filePaths.Length;
            StartCoroutine(GetAudio(index));
        }

        if (timeText != null && source.clip != null)
            timeText.text = ((int)(source.time / 60)).ToString("0") + ":" + ((int)(source.time % 60)).ToString("00") + " / " +
                ((int)(source.clip.length / 60)).ToString("0") + ":" + ((int)(source.clip.length % 60)).ToString("00");

        if (Input.GetButtonDown("Jump"))
        {
            if (source.isPlaying)
            {
                source.Pause();
                if (ps != null)
                {
                    ps.Pause();
                }
            }
            else
            {
                source.UnPause();
                if (ps != null)
                {
                    ps.Play();
                }
            }
        }
    }



    public void ChooseFolder()
    {
        string[] paths = StandaloneFileBrowser.OpenFolderPanel("Select Folder", "", true);

        if (paths.Length == 0)
            return;

        path = "";
        foreach (var p in paths)
        {
            path += p;
        }
        path += @"\";

        StartSong();
    }

    public void StartSong()
    {
        try
        {
            filePaths = Directory.GetFiles(path, "*." + fileType.ToString());
        }
        catch { }

        if (filePaths.Length > 0)
            StartCoroutine(GetAudio(index));
    }



    private IEnumerator GetAudio(int index)
    {
        www = new WWW(filePaths[index]);
        yield return www;

        if (www.error != null && www.error != "")
            Debug.Log(www.error);

        //source.clip = WWWAudioExtensions.GetAudioClip(www, false, true, AudioType.MPEG);
        source.clip = www.GetAudioClip(false, true, AudioType.OGGVORBIS);


        if (source.clip != null)
        {
            if (source.clip.loadState == AudioDataLoadState.Loaded)
            {
                source.Play();
            }
        }

        if (songText != null)
        {
            songText.text = Path.GetFileNameWithoutExtension(filePaths[index]);
        }

        StartCoroutine(BufferNextSong());
    }

    private IEnumerator BufferNextSong()
    {
        yield return new WaitForSeconds(source.clip.length + .05f);
        source.Stop();
        index = (index + 1) % filePaths.Length;
        StartCoroutine(GetAudio(index));
    }
}
