using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private EnemyMind enemyMind;

    private NavMeshAgent agent;

    private Health health;

    private List<Collider> ragdollParts = new List<Collider>();

    private void Start()
    {
        enemyMind = GetComponent<EnemyMind>();
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();

        enemyMind.OnChase.AddListener(Chase);
        enemyMind.OnReach.AddListener(Reach);
        health.OnSurvive.AddListener(HitReaction);

        SetRagdollParts();
    }

    private void SetRagdollParts()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider c in colliders)
        {
            if(c.gameObject != gameObject)
            {
                c.GetComponent<Rigidbody>().isKinematic = true;
                c.isTrigger = true;
                ragdollParts.Add(c);
            }
        }
    }

    public void TurnOnRagdoll()
    {
        agent.enabled = false;
        animator.enabled = false;

        foreach (Collider c in ragdollParts)
        {
            c.GetComponent<Rigidbody>().isKinematic = false;
            c.isTrigger = false;
        }
    }

    private void OnDestroy()
    {
        enemyMind.OnChase.RemoveListener(Chase);
        enemyMind.OnReach.RemoveListener(Reach);
        health.OnSurvive.RemoveListener(HitReaction);
    }

    private void Chase()
    {
        if (animator.enabled)
        {
            animator.SetBool("IsChasing", true);
            animator.SetBool("IsReach", false);
        }
    }

    private void Reach()
    {
        if (animator.enabled)
        {
            animator.SetBool("IsChasing", false);
            animator.SetBool("IsReach", true);
        }
    }

    private void Update()
    {
        if(animator.enabled)
        {
            animator.SetFloat("Velocity", agent.velocity.magnitude / agent.speed);
        }
    }

    private void HitReaction()
    {
        if (animator.enabled)
        {
            animator.Play("ReactionHit", animator.GetLayerIndex("Damage"), 0f);
        }
    }
}
