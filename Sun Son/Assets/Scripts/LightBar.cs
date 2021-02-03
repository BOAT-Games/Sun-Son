using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightBar : MonoBehaviour
{
    public Slider slider;
    public void SetMaxLightPoints(int lightPoints)
    {
        slider.maxValue = lightPoints;
    }

    public void SetLightPoints(int lightPoints)
    {
        slider.value = lightPoints;
    }
}
