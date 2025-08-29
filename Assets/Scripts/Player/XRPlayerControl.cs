using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRPlayerControl : MonoBehaviour
{
    [SerializeField] private GrabMoveProvider[] grabMoveProviders;
    [SerializeField] private Collider[] grabColliders;

    void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < grabColliders.Length; i++)
        {
            if (other == grabColliders[i])
            {
                SetGrabMovers(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < grabColliders.Length; i++)
        {
            if (other == grabColliders[i])
            {
                SetGrabMovers(false);
            }
        }
    }

    private void SetGrabMovers(bool isActive)
    {
        for (int i = 0; i < grabMoveProviders.Length; i++)
        {
            grabMoveProviders[i].enabled = isActive;
        }
    }
}
