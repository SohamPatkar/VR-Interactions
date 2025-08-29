using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WandControl : XRGrabInteractable
{
    [SerializeField] private Transform wandProjectileSpawnPoint;
    [SerializeField] private GameObject wandProjectile;

    private bool isFiring = false;

    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);

        if (wandProjectile != null)
        {
            isFiring = true;
            Instantiate(wandProjectile, wandProjectileSpawnPoint.position, wandProjectileSpawnPoint.rotation);
            SoundManager.Instance.PlayAudio(SoundType.SHOOT, false);
        }
    }

    protected override void OnDeactivated(DeactivateEventArgs args)
    {
        base.OnDeactivated(args);
        isFiring = false;
        SoundManager.Instance.StopAudio();
    }
}
