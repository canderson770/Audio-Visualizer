using System.Runtime.InteropServices;
using System.Text;

public class MusicPlayer
{
    private string Pcommand;
    public bool isOpen;

    [DllImport("winmm.dll")]
    private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, int bla);

    public MusicPlayer()
    {

    }

    /// <SUMMARY>
    /// Stops currently playing audio file
    /// </SUMMARY>
    public void Close()
    {
        Pcommand = "close MediaFile";
        mciSendString(Pcommand, null, 0, 0);
        isOpen = false;
    }

    /// <SUMMARY>
    /// Opens audio file to play
    /// </SUMMARY>
    public void Open(string sFileName)
    {
        Pcommand = "open \"" + sFileName + "\" type mpegvideo alias MediaFile";
        mciSendString(Pcommand, null, 0, 0);
        isOpen = true;
    }

    /// <SUMMARY>
    /// Plays selected audio file
    /// </SUMMARY>
    public void Play(bool loop)
    {
        if (isOpen)
        {
            Pcommand = "play MediaFile";
            if (loop)
                Pcommand += " REPEAT";
            mciSendString(Pcommand, null, 0, 0);
        }
    }

    /// <SUMMARY>
    /// Pauses currently playing audio file
    /// </SUMMARY>
    public void Pause()
    {
        Pcommand = "pause MediaFile";
        mciSendString(Pcommand, null, 0, 0);
    }
}
