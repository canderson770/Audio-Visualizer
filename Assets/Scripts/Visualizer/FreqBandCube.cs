using UnityEngine;

public class FreqBandCube : MonoBehaviour
{
    private float value;

    [SerializeField] private VisualizerVars variables;

    [Tooltip("Frequency to respond to")]
    [Range(1, 8)] public int band = 1;

    [Tooltip("Minimum vertical scale of frequency band")]
    public float minScale = 2.5f;

    [Tooltip("Multiplier for frequency band scale")]
    public float scaleMultiplier = 1.5f;

    [Tooltip("Smooth motion with buffer?")]
    public bool useBuffer = false;

    private void OnValidate()
    {
        if (variables == null)
            variables = (VisualizerVars)Resources.Load("VisualizerVars", typeof(VisualizerVars));
    }

    private void Update()
    {
        if (variables == null) return;

        if (useBuffer)
            value = (variables.bandBuffer[band - 1] * scaleMultiplier) + minScale;
        else
            value = (variables.freqBand[band - 1] * scaleMultiplier) + minScale;

        transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(value), transform.localScale.z);
    }
}
