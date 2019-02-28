using UnityEngine;
using UnityEngine.Events;

public abstract class MusicLoader : MonoBehaviour
{
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
}
