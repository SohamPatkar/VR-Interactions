using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class JoystickColorControls : MonoBehaviour
{
    [SerializeField] private XRBaseInteractable joystick;
    [SerializeField] private Renderer joystickRenderer;
    [SerializeField] private Material joystickActive;
    [SerializeField] private Material joystickInactive;

    void Start()
    {
        joystick.selectEntered.AddListener(JoystickEntered);
        joystick.selectExited.AddListener(JoystickExited);
    }

    private void JoystickExited(SelectExitEventArgs arg0)
    {
        joystickRenderer.material = joystickInactive;
    }

    private void JoystickEntered(SelectEnterEventArgs args)
    {
        joystickRenderer.material = joystickActive;
    }
}
