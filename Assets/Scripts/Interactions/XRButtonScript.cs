using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class XRButtonScript : XRSimpleInteractable
{
    [SerializeField] private Image button;
    [SerializeField] private Color highlightedColor;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color primaryColor;
    [SerializeField] private Color deselectColor;

    void Start()
    {
        button = GetComponent<Image>();

        ResetColor();
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);

        button.color = highlightedColor;
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);

        button.color = primaryColor;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        SoundManager.Instance.PlayAudio(SoundType.BUTTONCLICK, false);

        button.color = selectedColor;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        SoundManager.Instance.StopAudio();

        button.color = deselectColor;
    }

    public void ResetColor()
    {
        button.color = primaryColor;
    }
}
