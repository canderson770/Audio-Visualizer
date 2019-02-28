using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class AudioVisualizer : MonoBehaviour
{
    private AudioSource source;

    [SerializeField] private VisualizerVars variables;

    [SerializeField] private float decrease1 = .005f;
    [SerializeField] private float decrease2 = 1.2f;

    [Header("Debug")]
    public bool showDebug = false;

    private void Awake()
    {
        variables?.Reset();
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnValidate()
    {
        if (variables == null)
            variables = (VisualizerVars)Resources.Load("VisualizerVars", typeof(VisualizerVars));
    }

    private void Update()
    {
        if (variables == null) return;

        GetSpectrumAudioSource();
        GetFrequencyBands();
        BandBuffer();
    }

    private void GetSpectrumAudioSource()
    {
        source.GetSpectrumData(variables.samples, 0, FFTWindow.Blackman);
    }

    private void BandBuffer()
    {
        for (int i = 0; i < 8; i++)
        {
            if (variables.freqBand[i] > variables.bandBuffer[i])
            {
                variables.bandBuffer[i] = variables.freqBand[i];
                variables.bufferDecrease[i] = decrease1;
            }

            if (variables.freqBand[i] < variables.bandBuffer[i])
            {
                variables.bandBuffer[i] -= variables.bufferDecrease[i];
                variables.bufferDecrease[i] *= decrease2;
            }
        }
    }

    private void GetFrequencyBands()
    {
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0;

            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            if (i == 7)
                sampleCount += 2;

            for (int j = 0; j < sampleCount; j++)
            {
                average += variables.samples[count] * (count + 1);
                count++;
            }

            average /= count;
            variables.freqBand[i] = average * 10;
        }
    }

    private Rect windowRect = new Rect(20, 20, 200, 150);
    private void OnGUI()
    {
        if (showDebug == false) return;

        // Register the window. Notice the 3rd parameter
        windowRect = GUI.Window(0, windowRect, DoMyWindow, "Debug");
    }

    // Make the contents of the window
    private void DoMyWindow(int windowID)
    {
        GUILayout.Label("Source: " + (source ? "True" : "False"));
        if (variables == null) return;
        GUILayout.Label("Samples: " + variables.samples[0].ToString());
        GUILayout.Label("Frequency: " + variables.freqBand[0].ToString());
        GUILayout.Label("Band Buffer: " + variables.bandBuffer[0].ToString());
        GUILayout.Label("Buffer Decrease: " + variables.bufferDecrease[0].ToString());
    }
}
