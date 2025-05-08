using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movment Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Kill Settings")]
    [SerializeField] private float deathRadius = 2f;
    [SerializeField] private LayerMask cubeLayerMask;

    private NavMeshAgent agent;

    public bool showDeathZone = true;
    public Color deathZoneColor;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if(agent == null)
        {
            enabled = false;
            return;
        }

        agent.speed = moveSpeed;
        agent.angularSpeed = rotationSpeed;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        CheckDeathZone();
    }

    void CheckDeathZone()
    {
        Collider[] coliders = Physics.OverlapSphere(transform.position, deathRadius, cubeLayerMask);
        foreach(Collider colider in coliders)
        {
            ICubeDeath cubeDeath = colider.GetComponent<ICubeDeath>();
            if(cubeDeath != null) { cubeDeath.Die(); }
        }
    }


    private void OnDrawGizmos()
    {
        if(showDeathZone)
        {
            Gizmos.color = deathZoneColor;
            Gizmos.DrawSphere(transform.position, deathRadius);
        }
    }


}
