using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionControl : MonoBehaviour
{
    [Header("Emission Control")]

    [SerializeField]
    [Tooltip("Emission Intensity is how strong the emission is and used for debugging, Multiplier will be used with the samples")]
    float EmissionIntensity, Multiplier;
    [SerializeField]
    [Tooltip("Maximum emission intensity")]
    float Max;
    [SerializeField]
    [Tooltip("Which material the script should access for the emission")]
    Material material;
    [SerializeField]
    [Tooltip("Change the colour of the emission, emission map should be white")]
    Color emission = Color.white;
    [SerializeField]
    [Tooltip("Number of samples will change how smoothly emission reacts to audio")]
    float[] Samples;

    [Header("Audio Source")]
    [SerializeField]
    [Tooltip("The audio source to which the emission will react")]
    AudioSource source;


    private void Update()
    {
        source.GetOutputData(Samples, 0);
        float CurrentVol = 0f;
        foreach (float vol in Samples)
        {
            CurrentVol += Mathf.Abs(vol);
        }
        CurrentVol /= Samples.Length;
        EmissionIntensity = CurrentVol * Multiplier;
        if (EmissionIntensity > Max)
        {
            EmissionIntensity = Max;
        }
        Color EmissionColor = emission * Mathf.LinearToGammaSpace(EmissionIntensity);
        material.SetColor("_EmissionColor", EmissionColor);
    }
}
