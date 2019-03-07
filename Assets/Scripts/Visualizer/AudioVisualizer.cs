using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioVisualizer : MonoBehaviour
{
    private AudioSource source;

    [SerializeField] private VisualizerVars variables;
    [SerializeField] private float decrease1 = .005f;
    [SerializeField] private float decrease2 = 1.2f;

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
}
