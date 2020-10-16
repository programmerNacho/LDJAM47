using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoulBag : MonoBehaviour
{
    [SerializeField]
    private int maxCapacity = 20;

    private int currentCapacity;

    private void Start()
    {
        currentCapacity = 0;
    }

    public int AddSouls(int souls)
    {
        int totalSouls = currentCapacity + souls;
        int subtraction = totalSouls - maxCapacity;

        if(subtraction > 0)
        {
            currentCapacity = maxCapacity;
            return subtraction;
        }
        else
        {
            currentCapacity = totalSouls;
            return 0;
        }
    }
}
