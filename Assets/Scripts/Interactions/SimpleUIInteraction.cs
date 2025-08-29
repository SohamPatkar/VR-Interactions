using TMPro;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SimpleUIInteraction : MonoBehaviour
{
    public UnityEvent<string> OnStart;
    public UnityEvent<string> OnEnd;

    [SerializeField] private DrawerScript drawer;
    [SerializeField] private ExplosiveInteractable explosive;
    [SerializeField] private SliderLights sliderLight;
    [SerializeField] private XRButtonScript startButton;
    [SerializeField] private string startMessage;
    [SerializeField] private string completedMessage;

    void Start()
    {
        if (startButton != null)
        {
            startButton.selectEntered.AddListener(OnStartPressed);
        }

        if (drawer != null)
        {
            drawer.OnChallengeCompleted += OnChallengeCompleted;
        }

        if (explosive != null)
        {
            explosive.OnChallengeCompleted += OnChallengeCompleted;
        }

        if (sliderLight != null)
        {
            sliderLight.OnCompleted += OnChallengeCompleted;
        }
    }

    private void OnStartPressed(SelectEnterEventArgs arg0)
    {
        OnStart?.Invoke(startMessage);
    }

    private void OnChallengeCompleted()
    {
        OnEnd?.Invoke(completedMessage);
        SoundManager.Instance.PlayAudio(SoundType.COMPLETED, false);
    }
}
