using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveToClick : MonoBehaviour
{
    Camera myCamera;
    NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        myCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && 
            Physics.Raycast(myCamera.ScreenPointToRay(Input.mousePosition), out var hit, 100))
            agent.SetDestination(hit.point);
    }
}
