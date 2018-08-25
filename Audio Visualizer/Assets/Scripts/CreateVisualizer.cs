using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Light))]
public class CreateVisualizer : MonoBehaviour
{
    GameObject[] prefabs = new GameObject[512];
    float rotationAmount;

    public GameObject prefab;
    public float distance = 100;
    public float maxScale = 1000;
    public float baseScale = 1;
    [Range(0, 10)]
    public float scaleMultiplier = 100;
    [Range(1, 512)]
    public int scaleBand = 6;

    [Space(10)]
    public Light thisLight;
    [Range(0, 1000)]
    public float lightIntensity = 100;
    [Range(1, 8)]
    public int lightIntensityBand = 1;
    //[Range(0, 1000)]
    //public float lightRange = 100;

    [Space(10)]
    public Camera thisCamera;
    [Range(0, 100)]
    public float cameraMultiplier = 2;
    [Range(1, 512)]
    public int cameraBand = 1;

    [Space(10)]
    public ParticleSystem ps;
    public float psIntensity = 100;
    [Range(1, 8)]
    public int psBand = 1;


    void Start()
    {
        if (thisLight == null)
            thisLight = GetComponent<Light>();

        if (thisCamera == null)
            thisCamera = Camera.main;

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
                instance.transform.position = Vector3.forward * distance;
                prefabs[i] = instance;
            }
        }
        else
        {
            Debug.LogWarning("Prefab is not set on " + transform.name);
        }
    }

    void Update()
    {
        for (int i = 0; i < 512; i++)
        {
            if (prefabs[i] != null)
                prefabs[i].transform.localScale = new Vector3(1, AudioVisualizer.samples[i] * maxScale + baseScale, 1);
        }

        if (thisLight != null)
            thisLight.intensity = AudioVisualizer.bandBuffer[lightIntensityBand-1] * lightIntensity;
        //thisLight.range = (AudioVisualizer.samples[5] * lightRange) + 30;

        transform.localScale = (Vector3.one * AudioVisualizer.samples[scaleBand-1] * scaleMultiplier) + Vector3.one;

        thisCamera.fieldOfView = (AudioVisualizer.samples[cameraBand-1] * -cameraMultiplier) + 60;

        if (ps != null)
        {
            ParticleSystem.NoiseModule noiseModule = ps.noise;
            noiseModule.strength = AudioVisualizer.bandBuffer[psBand - 1] * psIntensity + 1;
        }
    }
}
