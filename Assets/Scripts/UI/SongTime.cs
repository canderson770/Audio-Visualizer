public class SongTime : SetText
{
    private void OnEnable()
    {
        MusicLoader.SongTime += SetTime;
    }
    private void OnDisable()
    {
        MusicLoader.SongTime -= SetTime;
    }

    private void SetTime(float currentTime, float maxTime)
    {
        if (textComponent == null) return;
        textComponent.text = ((int)(currentTime / 60)).ToString("0") + ":" + ((int)(currentTime % 60)).ToString("00") + " / " +
            ((int)(maxTime / 60)).ToString("0") + ":" + ((int)(maxTime % 60)).ToString("00");
    }
}
