using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreqBandCube : MonoBehaviour
{
    [Range(1, 8)]
    public int band = 1;
    public float startScale = 1;
    public float scaleMultiplier = 10;
    public bool useBuffer;

    private float value;

    void Update()
    {
        if (useBuffer)
            value = (AudioVisualizer.bandBuffer[band - 1] * scaleMultiplier) + startScale;
        else
            value = (AudioVisualizer.freqBand[band - 1] * scaleMultiplier) + startScale;

        transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(value), transform.localScale.z);
    }
}
