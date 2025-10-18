using UnityEngine;
using UnityEngine.AI;


public class Inimigo : MonoBehaviour
{
    [SerializeField] Transform alvo;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }


    void Update()
    {
        agent.SetDestination(alvo.position);
    }
}
