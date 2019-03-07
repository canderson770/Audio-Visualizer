using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class MusicControls : MonoBehaviour
{
    public static UnityAction PlayPause;
    public static UnityAction<bool> Skip;
    public static UnityAction<float> FastForward;

    public void BUTTON_PlayPause()
    {
        PlayPause?.Invoke();
    }

    public void BUTTON_Next(bool reverse)
    {
        Skip?.Invoke(reverse);
    }

    public void BUTTON_FastForward(float time)
    {
        FastForward?.Invoke(time);
    }
}
