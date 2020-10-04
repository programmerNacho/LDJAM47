using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GunCooldownSystem : MonoBehaviour
{
    [SerializeField]
    private float maxHeat = 10;
    [SerializeField]
    private float timeToStartCoolingAfterLastHeat = 0.5f;
    [SerializeField]
    private float maxCooldownSpeed = 5f;
    [SerializeField]
    private float cooldownAcceleration;

    private float currentHeat;
    public float CurrentHeat
    {
        get
        {
            return currentHeat;
        }

        private set
        {
            currentHeat = value;
        }
    }

    public float MaxHeat
    {
        get
        {
            return maxHeat;
        }

        private set
        {
            maxHeat = value;
        }
    }

    private float timePassedAfterLastHeat;
    private float currentCooldownSpeed;

    public UnityEvent OnHeat = new UnityEvent();
    public UnityEvent OnCool = new UnityEvent();
    public UnityEvent OnOverheat = new UnityEvent();
    public UnityEvent OnCoolComplete = new UnityEvent();

    private void Start()
    {
        currentHeat = 0f;
        timePassedAfterLastHeat = 0f;
        currentCooldownSpeed = 0f;
    }

    private void Update()
    {
        if(currentHeat > 0f)
        {
            timePassedAfterLastHeat += Time.deltaTime;
            if(timePassedAfterLastHeat >= timeToStartCoolingAfterLastHeat)
            {
                currentCooldownSpeed = Mathf.Clamp(currentCooldownSpeed + cooldownAcceleration * Time.deltaTime, 0f, maxCooldownSpeed);
            }
            currentHeat = Mathf.Clamp(currentHeat - currentCooldownSpeed * Time.deltaTime, 0f, maxHeat);
            CheckIfCoolComplete();
        }
    }

    private void CheckIfCoolComplete()
    {
        if (currentHeat <= 0f)
        {
            timePassedAfterLastHeat = 0f;
            currentCooldownSpeed = 0f;
            OnCoolComplete.Invoke();
        }
    }

    private void CheckIfOverheat()
    {
        if (currentHeat >= maxHeat)
        {
            OnOverheat.Invoke();
        }
    }

    public void Heat(float heat)
    {
        currentHeat = Mathf.Clamp(currentHeat + Mathf.Abs(heat), 0f, maxHeat);
        OnHeat.Invoke();
        CheckIfOverheat();
        timePassedAfterLastHeat = 0f;
        currentCooldownSpeed = 0f;
    }

    public void Cool(float cool)
    {
        currentHeat = Mathf.Clamp(currentHeat - Mathf.Abs(cool), 0f, maxHeat);
        OnCool.Invoke();
        CheckIfCoolComplete();
    }
}
