using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public abstract class SimpleHingeInteractable : XRSimpleInteractable
{
    public Transform grabHandle;
    [SerializeField] private bool isLocked = true;
    [SerializeField] public Vector3 handPositionLimits;

    private SphereCollider hingeCollider;
    private Vector3 hingePosition;

    protected const string defaultLayerMask = "Default";
    protected const string grabLayerMask = "Grabable";

    protected virtual void Start()
    {
        hingeCollider = GetComponent<SphereCollider>();
    }

    protected virtual void Update()
    {
        if (grabHandle != null)
        {
            TrackHand();
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (!isLocked)
        {
            grabHandle = args.interactorObject.transform;
            SoundManager.Instance.PlayAudio(SoundType.OPENED, true);
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        SetLayerMask(grabLayerMask);
        grabHandle = null;

        SoundManager.Instance.StopAudio();

        ResetHinge();
    }

    protected void SetLayerMask(string mask)
    {
        interactionLayers = InteractionLayerMask.GetMask(mask);
    }

    private void TrackHand()
    {
        transform.LookAt(grabHandle, transform.forward);
        hingePosition = hingeCollider.bounds.center;

        if (grabHandle.position.z > hingePosition.z + handPositionLimits.z || grabHandle.position.z < hingePosition.z - handPositionLimits.z)
        {
            SetLayerMask(defaultLayerMask);
            Debug.Log("Released--- Z");
        }
        else if (grabHandle.position.x > hingePosition.x + handPositionLimits.x || grabHandle.position.x < hingePosition.x - handPositionLimits.x)
        {
            SetLayerMask(defaultLayerMask);
            Debug.Log("Released--- X");
        }
        else if (grabHandle.position.y > hingePosition.y + handPositionLimits.y || grabHandle.position.y < hingePosition.y - handPositionLimits.y)
        {
            SetLayerMask(defaultLayerMask);
            Debug.Log("Released--- Y");
        }
    }

    protected virtual void ResetHinge() { }

    public void UnlockedDoors()
    {
        isLocked = false;
    }
}
