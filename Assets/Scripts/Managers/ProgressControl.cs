using UnityEngine;
using TMPro;


public class ProgressControl : MonoBehaviour
{
    [SerializeField] private SimpleUIInteraction[] uiEvents;
    [SerializeField] private TextMeshProUGUI[] textFields;
    [SerializeField] private Light keyIndicatorLight;

    void OnEnable()
    {
        for (int i = 0; i < uiEvents.Length; i++)
        {
            if (uiEvents[i] != null)
            {
                uiEvents[i].OnStart.AddListener(SetText);
                uiEvents[i].OnEnd.AddListener(SetText);
            }
            else
            {
                Debug.Log("UI events is null");
            }
        }
    }

    private void SetText(string text)
    {
        for (int i = 0; i < textFields.Length; i++)
        {
            textFields[i].text = text;
        }

        if (keyIndicatorLight != null)
        {
            SetKeyIndicator();
        }
    }

    private void SetKeyIndicator()
    {
        if (keyIndicatorLight.enabled)
        {
            keyIndicatorLight.enabled = false;
        }
        else
        {
            keyIndicatorLight.enabled = true;
        }
    }
}

