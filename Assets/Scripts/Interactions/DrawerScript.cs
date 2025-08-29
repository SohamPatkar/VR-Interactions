using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DrawerScript : XRGrabInteractable
{
    public event Action OnChallengeCompleted;

    [SerializeField] private Transform drawer;
    [SerializeField] private XRSocketInteractor xRDirectInteractor;
    [SerializeField] private bool isLocked;
    [SerializeField] private Vector3 limitPosition;
    [SerializeField] private Light keyIndicatorLight;

    private const string defaultLayerMask = "Default";
    private const string grabLayerMask = "Grabable";
    private Transform transformParent;
    private bool isGrabbed = false;
    private Vector3 initialPosition;
    private Vector3 drawerInitialPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (xRDirectInteractor != null)
        {
            xRDirectInteractor.selectEntered.AddListener(OnKeyEntered);
            xRDirectInteractor.selectExited.AddListener(OnKeyExited);
        }

        transformParent = transform.parent.transform;
        initialPosition = transform.localPosition;
        drawerInitialPosition = drawer.localPosition;
    }

    private void OnKeyExited(SelectExitEventArgs arg)
    {
        isLocked = true;
    }

    private void OnKeyEntered(SelectEnterEventArgs arg)
    {
        isLocked = false;

        OnChallengeCompleted.Invoke();
        SoundManager.Instance.PlayAudio(SoundType.BUTTONCLICK, false);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (!isLocked)
        {
            transform.parent = transformParent;
            isGrabbed = true;
            SoundManager.Instance.PlayAudio(SoundType.DRAWERSLIDE, true);
        }
        else
        {
            SetLayerMask(defaultLayerMask);
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        SetLayerMask(grabLayerMask);
        isGrabbed = false;
        SoundManager.Instance.StopAudio();
        transform.localPosition = drawer.localPosition;
    }

    void Update()
    {
        if (isGrabbed && drawer != null)
        {
            drawer.localPosition = new Vector3(drawer.localPosition.x, drawer.localPosition.y, transform.localPosition.z);

            CheckLimits();
        }
    }

    private void CheckLimits()
    {
        if (transform.localPosition.x >= limitPosition.x || transform.localPosition.x <= -limitPosition.x)
        {
            ResetDrawerHandleLayer();
        }
        else if (transform.localPosition.y >= limitPosition.y || transform.localPosition.y <= -limitPosition.y)
        {
            ResetDrawerHandleLayer();

        }
        else if (drawer.localPosition.z >= limitPosition.z || drawer.localPosition.z <= drawerInitialPosition.z - 0.1)
        {
            isGrabbed = false;
            ResetDrawerHandleLayer();
            drawer.localPosition = drawerInitialPosition;
        }
    }

    private void ResetDrawerHandleLayer()
    {
        SetLayerMask(defaultLayerMask);
    }

    private void SetLayerMask(string mask)
    {
        interactionLayers = InteractionLayerMask.GetMask(mask);
    }
}
