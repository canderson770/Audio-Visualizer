public class SongName : SetText
{
    private void OnEnable()
    {
        MusicLoader.SongName += SetSongName;
    }
    private void OnDisable()
    {
        MusicLoader.SongName -= SetSongName;
    }

    private void SetSongName(string name)
    {
        if (textComponent != null)
            textComponent.text = name;
    }
}
