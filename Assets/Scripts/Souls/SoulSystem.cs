using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulSystem : MonoBehaviour
{
    [SerializeField]
    private PlayerSoulBag playerSoulBag;
    [SerializeField]
    private float minDistanceToPlayer = 5f;
    [SerializeField]
    private List<SoulFeedObject> soulFeedObjects;
    [SerializeField]
    private float minDistanceToSoulFeedObject = 5f;

    private int currentSoulsUsed;

    public void ZombieKilled(Vector3 zombiePosition, int souls)
    {
        if(Vector3.Distance(zombiePosition, playerSoulBag.transform.position) <= minDistanceToPlayer)
        {
            souls = playerSoulBag.AddSouls(souls);

            if (souls > 0)
            {
                List<SoulFeedObject> nearEnoughSoulFeedObjects = new List<SoulFeedObject>();

                foreach (SoulFeedObject s in soulFeedObjects)
                {
                    if (Vector3.Distance(s.transform.position, zombiePosition) <= minDistanceToSoulFeedObject)
                    {
                        nearEnoughSoulFeedObjects.Add(s);
                    }
                }

                for (int i = 0; i < nearEnoughSoulFeedObjects.Count; i++)
                {
                    float nearestDistance = float.MaxValue;
                    int nearestIndex = -1;

                    for (int j = i; j < nearEnoughSoulFeedObjects.Count; j++)
                    {
                        float distance = Vector3.Distance(nearEnoughSoulFeedObjects[j].transform.position, zombiePosition);

                        if (distance < nearestDistance)
                        {
                            nearestIndex = j;
                        }
                    }

                    SoulFeedObject aux = nearEnoughSoulFeedObjects[i];
                    nearEnoughSoulFeedObjects[i] = nearEnoughSoulFeedObjects[nearestIndex];
                    nearEnoughSoulFeedObjects[nearestIndex] = aux;
                }

                foreach (SoulFeedObject s in nearEnoughSoulFeedObjects)
                {
                    int previousSouls = souls;

                    souls = s.AddSouls(souls);

                    int soulsUsed = previousSouls - souls;

                    currentSoulsUsed += soulsUsed;

                    if (souls <= 0)
                    {
                        return;
                    }
                }

                if (souls > 0)
                {
                    currentSoulsUsed += souls;
                }
            }
        }
        else
        {
            List<SoulFeedObject> nearEnoughSoulFeedObjects = new List<SoulFeedObject>();

            foreach (SoulFeedObject s in soulFeedObjects)
            {
                if (Vector3.Distance(s.transform.position, zombiePosition) <= minDistanceToSoulFeedObject)
                {
                    nearEnoughSoulFeedObjects.Add(s);
                }
            }

            for (int i = 0; i < nearEnoughSoulFeedObjects.Count; i++)
            {
                float nearestDistance = float.MaxValue;
                int nearestIndex = -1;

                for (int j = i; j < nearEnoughSoulFeedObjects.Count; j++)
                {
                    float distance = Vector3.Distance(nearEnoughSoulFeedObjects[j].transform.position, zombiePosition);

                    if (distance < nearestDistance)
                    {
                        nearestIndex = j;
                    }
                }

                SoulFeedObject aux = nearEnoughSoulFeedObjects[i];
                nearEnoughSoulFeedObjects[i] = nearEnoughSoulFeedObjects[nearestIndex];
                nearEnoughSoulFeedObjects[nearestIndex] = aux;
            }

            foreach (SoulFeedObject s in nearEnoughSoulFeedObjects)
            {
                int previousSouls = souls;

                souls = s.AddSouls(souls);

                int soulsUsed = previousSouls - souls;

                currentSoulsUsed += soulsUsed;

                if (souls <= 0)
                {
                    return;
                }
            }

            if (souls > 0)
            {
                currentSoulsUsed += souls;
            }
        }
    }
}
