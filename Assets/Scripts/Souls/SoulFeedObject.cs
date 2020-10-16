using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoulFeedObject : MonoBehaviour
{
    [SerializeField]
    private int soulsNeeded = 20;

    private int currentSouls;

    public UnityEvent OnSoulsCompleted = new UnityEvent();

    private void Start()
    {
        currentSouls = 0;
    }

    public int AddSouls(int souls)
    {
        int totalSouls = currentSouls + souls;
        int subtraction = totalSouls - soulsNeeded;

        if (subtraction > 0)
        {
            currentSouls = soulsNeeded;
            OnSoulsCompleted.Invoke();
            return subtraction;
        }
        else
        {
            currentSouls = totalSouls;
            return 0;
        }
    }
}
