using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioStreamMP3 : MonoBehaviour
{
    public string path;
    public string[] filePaths;
    public enum AudioFileType { OGG, MP3 };
    public AudioFileType fileType = AudioFileType.MP3;
    public Text songText;
    public Text timeText;
    public ParticleSystem ps;

    MusicPlayerWindows musicPlayer = new MusicPlayerWindows();
    AudioSource source;
    int index = 0;

    public bool isPaused = false;
    public bool loopSong = false;

    void Start()
    {
        source = GetComponent<AudioSource>();
        filePaths = Directory.GetFiles(path, "*." + fileType.ToString());

        if (filePaths.Length > 0)
            GetAudio(index);
    }

    public void OnApplicationFocus(bool focus)
    {
        if (focus)
            UnPause();
        else
            Pause();
    }

    public void OnApplicationPause(bool pause)
    {
        if (pause)
            Pause();
        else
            UnPause();
    }

    public void OnDisable()
    {
        if (musicPlayer.isOpen)
            musicPlayer.Close();
    }

    private void Pause()
    {
        if (musicPlayer.isOpen)
            musicPlayer.Pause();

        if (ps != null)
            ps.Pause();
    }

    private void UnPause()
    {
        if (musicPlayer.isOpen)
            musicPlayer.Play(loopSong);

        if (ps != null)
            ps.Play();
    }

    void GetAudio(int index)
    {
        musicPlayer.Open(filePaths[index]);
        musicPlayer.Play(loopSong);

        //byte[] b = new byte[128];
        //string sTitle = null;
        //string sSinger = null;
        //string sAlbum = null;
        //string sYear = null;
        //string sComm = null;

        //FileStream fs = new FileStream(filePaths[index], FileMode.Open);
        //fs.Seek(-128, SeekOrigin.End);
        //fs.Read(b, 0, 128);
        //bool isSet = false;
        //string sFlag = System.Text.Encoding.Default.GetString(b, 0, 3);
        //if (sFlag.CompareTo("TAG") == 0)
        //{
        //    System.Console.WriteLine("Tag   is   setted! ");
        //    isSet = true;
        //}

        //if (isSet)
        //{
        //    //get   title   of   song; 
        //    sTitle = System.Text.Encoding.Default.GetString(b, 3, 30);
        //    System.Console.WriteLine("Title: " + sTitle);
        //    //get   singer; 
        //    sSinger = System.Text.Encoding.Default.GetString(b, 33, 30);
        //    System.Console.WriteLine("Singer: " + sSinger);
        //    //get   album; 
        //    sAlbum = System.Text.Encoding.Default.GetString(b, 63, 30);
        //    System.Console.WriteLine("Album: " + sAlbum);
        //    //get   Year   of   publish; 
        //    sYear = System.Text.Encoding.Default.GetString(b, 93, 4);
        //    System.Console.WriteLine("Year: " + sYear);
        //    //get   Comment; 
        //    sComm = System.Text.Encoding.Default.GetString(b, 97, 30);
        //    System.Console.WriteLine("Comment: " + sComm);
        //}

        //source.clip = musicPlayer

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
            GetAudio(index);
        }

        if (timeText != null && source.clip != null)
            timeText.text = ((int)(source.time / 60)).ToString("0") + ":" + ((int)(source.time % 60)).ToString("00") + " / " +
                ((int)(source.clip.length / 60)).ToString("0") + ":" + ((int)(source.clip.length % 60)).ToString("00");

        if (Input.GetButtonDown("Jump"))
        {
            isPaused = !isPaused;

            if (isPaused)
                Pause();
            else
                UnPause();
        }
    }


    IEnumerator BufferNextSong()
    {
        if (source.clip != null)
        {
            yield return new WaitForSeconds(source.clip.length + .05f);
            source.Stop();
            index = (index + 1) % filePaths.Length;
            GetAudio(index);
        }
    }
}
