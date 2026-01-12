using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    [SerializeField] Material sky;
    [SerializeField] Light sun;

    private float fullIntensity;
    private float cloudValue = 0f;

    private void Start()
    {
        fullIntensity = sun.intensity;
    }

    private void Update()
    {
        SetOvercast(cloudValue);
        cloudValue += .005f;
    }

    private void SetOvercast(float value)
    {
        sky.SetFloat("_Blend", value);
        sun.intensity = fullIntensity - (fullIntensity * value);
    }
}
