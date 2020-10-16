using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private NavMeshAgent navMeshAgent;

    private Health health;

    private void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
        health.OnSurvive.AddListener(OnSurvive);
    }

    private void OnDestroy()
    {
        health.OnSurvive.RemoveListener(OnSurvive);
    }

    public void Initialize(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        if(navMeshAgent.enabled && target)
        {
            navMeshAgent.SetDestination(target.position);
        }
    }

    private void OnSurvive()
    {
        if(navMeshAgent.enabled)
        {
            navMeshAgent.velocity = Vector3.zero;
        }
    }
}
