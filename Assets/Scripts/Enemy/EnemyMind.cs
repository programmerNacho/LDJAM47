using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyMind : Damager
{
    [SerializeField]
    private Health objective;
    [SerializeField]
    private float distanceToAttack = 2f;
    [SerializeField]
    private float timeToAttack = 0.5f;

    public enum Type { Chasing, Attacking }

    private NavMeshAgent navMeshAgent;

    private Type currentState;

    public Type CurrentState
    {
        get
        {
            return currentState;
        }

        private set
        {
            currentState = value;
        }
    }

    private float timeAttacking = 0f;

    public UnityEvent OnReach = new UnityEvent();
    public UnityEvent OnAttack = new UnityEvent();
    public UnityEvent OnChase = new UnityEvent();

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = distanceToAttack - 0.5f;
        currentState = Type.Chasing;
    }

    private void Update()
    {
        if(Vector3.Distance(objective.transform.position, transform.position) <= distanceToAttack)
        {
            currentState = Type.Attacking;
            timeAttacking += Time.deltaTime;
            OnReach.Invoke();
            if (timeAttacking >= timeToAttack)
            {
                timeAttacking = 0f;
                OnAttack.Invoke();
                base.Damage(objective);
            }
        }
        else
        {
            currentState = Type.Chasing;
            timeAttacking = 0f;
            OnChase.Invoke();
        }
    }
}
