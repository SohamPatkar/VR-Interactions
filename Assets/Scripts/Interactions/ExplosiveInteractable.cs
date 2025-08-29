using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class ExplosiveInteractable : XRGrabInteractable
{
    [SerializeField] public bool isActive = false;

    public event Action<int> OnDetonated;
    public event Action OnChallengeCompleted;

    private int powerExplosion = 1000;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        SoundManager.Instance.PlayAudio(SoundType.GRAB, false);

        if (args.interactorObject.transform.GetComponent<XRSocketInteractor>() != null)
        {
            isActive = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isActive && collision.gameObject.GetComponent<WandProjectile>() != null)
        {
            OnDetonated?.Invoke(powerExplosion);
            OnChallengeCompleted?.Invoke();
        }
    }
}
