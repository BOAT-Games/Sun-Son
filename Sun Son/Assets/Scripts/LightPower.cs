using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPower : MonoBehaviour
{
    public Light plight;

    private void Start()
    {
        plight = this.GetComponent<Light>();
    }

    public void SetMaxLightPoints(int lightPoints)
    {
        plight.range = ((float)lightPoints/100.0f) * 14;
        plight.intensity = ((float)lightPoints / 100.0f) * 1;
    }

    public void SetLightPoints(int lightPoints)
    {
        plight.range = ((float)lightPoints / 100.0f) * 14;
        plight.intensity = ((float)lightPoints / 100.0f) * 1;
    }
}
