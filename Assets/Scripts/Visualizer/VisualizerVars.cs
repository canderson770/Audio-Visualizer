using UnityEngine;

[CreateAssetMenu(fileName = "VisualizerVars")]
public class VisualizerVars : ScriptableObject
{
    [ReadOnly] public float[] samples = new float[512];
    [ReadOnly] public float[] freqBand = new float[8];
    [ReadOnly] public float[] bandBuffer = new float[8];
    [ReadOnly] public float[] bufferDecrease = new float[8];

    public void Reset()
    {
        samples = new float[512];
        freqBand = new float[8];
        bandBuffer = new float[8];
        bufferDecrease = new float[8];
    }
}
