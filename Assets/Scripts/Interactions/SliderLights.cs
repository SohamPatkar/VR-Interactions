using UnityEngine;
using UnityEngine.UI;
using System;

public class SliderLights : MonoBehaviour
{
    public event Action OnCompleted;

    [SerializeField] private Slider slider;
    [SerializeField] private float maxValue;
    [SerializeField] private float minValue;

    [SerializeField] private Light[] lights;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = maxValue;
        slider.minValue = minValue;

        slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(float arg0)
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].intensity = slider.value;
        }

        if (slider.value == maxValue)
        {
            OnCompleted?.Invoke();
        }
    }
}
