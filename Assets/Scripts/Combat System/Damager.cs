using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField]
    protected int damagePoints;

    public virtual void Damage(Health health)
    {
        health.ReceiveDamage(damagePoints);
    }
}
