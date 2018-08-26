using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateVisualizer : MonoBehaviour
{
    private GameObject[] prefabs = new GameObject[512];
    private float rotationAmount;

    [Header("Ring Height Settings")]

    [Tooltip("Reference to frequency band prefab")]
    public GameObject prefab;

    [Tooltip("Radius of ring")]
    public float radius = 100;

    [Tooltip("Minimum vertical scale of frequency band")]
    public float minScale = 1;

    [Tooltip("Maximum vertical scale of frequency band")]
    public float maxScale = 150;

    //[Tooltip("Random noise to make less uniform")]
    //public float randomNoise = 1;

    [Tooltip("Multiplier for frequency band scale")]
    public float scaleMultiplier = 1000;


    [Header("Ring Expand Settings")]

    [Tooltip("Multiplier for entire ring to scale to")]
    [Range(0, 1)] public float ringScaleMultiplier = 0.1f;

    [Tooltip("Frequency to respond to")]
    [Range(1, 512)] public int scaleBand = 6;


    [Header("Light Settings")]

    [Tooltip("Reference to Light")]
    public Light thisLight;

    [Tooltip("Multiplier for light intensity change")]
    [Range(0, 1000)] public float lightMultiplier = 0.2f;

    [Tooltip("Frequency to respond to")]
    [Range(1, 8)] public int lightIntensityBand = 1;


    [Header("Camera Settings")]

    [Tooltip("Reference to Camera")]
    public Camera thisCamera;

    [Tooltip("Multiplier for camera zoom")]
    [Range(0, 100)] public float cameraMultiplier = 2;

    [Tooltip("Frequency to respond to")]
    [Range(1, 512)] public int cameraBand = 1;


    [Header("Particle Settings")]

    [Tooltip("Reference to Particle System")]
    public ParticleSystem ps;

    [Tooltip("Multiplier for particle noise")]
    public float psIntensity = 100;

    [Tooltip("Frequency to respond to")]
    [Range(1, 8)] public int psBand = 1;


    private void Start()
    {
        //  get camera
        if (thisCamera == null)
            thisCamera = Camera.main;

        //  create ring
        rotationAmount = 360f / 512f;

        if (prefab != null)
        {
            for (int i = 0; i < 512; i++)
            {
                GameObject instance = Instantiate(prefab) as GameObject;
                instance.transform.position = transform.position;
                instance.transform.parent = transform;
                instance.name = prefab.name + i;
                transform.eulerAngles = new Vector3(0, -rotationAmount * i, 0);
                instance.transform.position = Vector3.forward * radius;
                prefabs[i] = instance;
            }
        }
        else
        {
            Debug.LogWarning("Prefab is not set on " + transform.name);
        }
    }

    private void Update()
    {
        //  ring vertical scale
        for (int i = 0; i < 512; i++)
        {
            float vertical = Mathf.Clamp(AudioVisualizer.samples[i] * scaleMultiplier + minScale, 0, maxScale);
            float randomNum = Random.Range(0, vertical/10);

            if (prefabs[i] != null)
                prefabs[i].transform.localScale = new Vector3(1, vertical + randomNum, 1);
        }

        //  ring horizontal cale
        transform.localScale = (Vector3.one * AudioVisualizer.samples[scaleBand - 1] * ringScaleMultiplier) + Vector3.one;

        //  light intensity
        if (thisLight != null)
            thisLight.intensity = AudioVisualizer.bandBuffer[lightIntensityBand - 1] * lightMultiplier;

        //  camera zoom
        thisCamera.fieldOfView = (AudioVisualizer.samples[cameraBand - 1] * -cameraMultiplier) + 60;

        //  particle effects
        if (ps != null)
        {
            ParticleSystem.NoiseModule noiseModule = ps.noise;
            noiseModule.strength = AudioVisualizer.bandBuffer[psBand - 1] * psIntensity + 1;
        }
    }
}
