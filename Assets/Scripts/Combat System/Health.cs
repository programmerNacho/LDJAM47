using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    protected int initialHealthPoints;
    [SerializeField]
    protected int maxHealthPoints;

    protected int currentHealthPoints;

    public UnityEvent OnHeal = new UnityEvent();
    public UnityEvent OnSurvive = new UnityEvent();
    public UnityEvent OnDie = new UnityEvent();

    protected virtual void Start()
    {
        currentHealthPoints = initialHealthPoints;
    }

    public virtual void Heal(int healthPoints)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints + healthPoints, 0, maxHealthPoints);
        OnHeal.Invoke();
    }

    public virtual void ReceiveDamage(int damagePoints)
    {
        if(currentHealthPoints > 0)
        {
            currentHealthPoints = Mathf.Clamp(currentHealthPoints - damagePoints, 0, maxHealthPoints);

            if (currentHealthPoints > 0)
            {
                OnSurvive.Invoke();
            }
            else
            {
                OnDie.Invoke();
            }
        }
    }
}
