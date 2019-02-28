using UnityEngine;

public class FreqBandCube : MonoBehaviour
{
    private float value;

    [Tooltip("Frequency to respond to")]
    [Range(1, 8)] public int band = 1;

    [Tooltip("Minimum vertical scale of frequency band")]
    public float minScale = 2.5f;

    [Tooltip("Multiplier for frequency band scale")]
    public float scaleMultiplier = 1.5f;

    [Tooltip("Smooth motion with buffer?")]
    public bool useBuffer = false;

    private void Update()
    {
        if (useBuffer)
            value = (AudioVisualizer.bandBuffer[band - 1] * scaleMultiplier) + minScale;
        else
            value = (AudioVisualizer.freqBand[band - 1] * scaleMultiplier) + minScale;

        transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(value), transform.localScale.z);
    }
}
