using UnityEngine;

public class WandProjectile : MonoBehaviour
{
    [SerializeField] private int power;
    [SerializeField] private float destroyTimer;

    private Rigidbody projectileRb;
    // Start is called before the first frame update
    void Start()
    {
        projectileRb = GetComponent<Rigidbody>();

        projectileRb.AddForce(transform.forward * power);

        Destroy(gameObject, destroyTimer);
    }
}
