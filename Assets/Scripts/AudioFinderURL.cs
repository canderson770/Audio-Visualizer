using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioFinderURL : MonoBehaviour
{
    public string path;
    public string[] filePaths;
    public enum AudioFileType { OGG, MP3 };
    public AudioFileType fileType = AudioFileType.OGG;
    public Text songText;
    public Text timeText;
    public ParticleSystem ps;

    AudioSource source;
    WWW www;
    int index = 0;

    void Start()
    {
        source = GetComponent<AudioSource>();
        filePaths = Directory.GetFiles(path, "*." + fileType.ToString());

        if (filePaths.Length > 0)
            StartCoroutine(GetAudio(index));
    }

    IEnumerator GetAudio(int index)
    {
        WWW www = new WWW(filePaths[index]);
        yield return www;

        if (www.error != null && www.error != "")
            Debug.Log(www.error);

        source.clip = www.GetAudioClip(false, true);

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

    void Update()
    {
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

    IEnumerator BufferNextSong()
    {
        yield return new WaitForSeconds(source.clip.length + .05f);
        source.Stop();
        index = (index + 1) % filePaths.Length;
        StartCoroutine(GetAudio(index));
    }
}
