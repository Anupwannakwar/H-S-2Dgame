using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider slider;
    public Gradient healthgradient;
    public Image fill;

    public void setmaxhealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = healthgradient.Evaluate(1.0f);
    }
    public void sethealth(int health)
    {
        slider.value = health;
        fill.color = healthgradient.Evaluate(slider.normalizedValue);
    }
}
