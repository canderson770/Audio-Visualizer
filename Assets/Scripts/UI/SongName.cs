public class SongName : SetText
{
    private void OnEnable()
    {
        MusicPlayer.SongName += SetSongName;
    }
    private void OnDisable()
    {
        MusicPlayer.SongName -= SetSongName;
    }

    private void SetSongName(string name)
    {
        if (textComponent != null)
            textComponent.text = name;
    }
}
