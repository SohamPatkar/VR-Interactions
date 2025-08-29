using UnityEngine;
using UnityEngine.AI;

public class RobotNavigation : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void Move(Vector3 move)
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.destination = navMeshAgent.transform.position + move;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(collision.gameObject);
        }
    }
}
