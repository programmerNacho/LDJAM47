using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SpawnerEvent : UnityEvent<EnemyMovement> { }

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private EnemyMovement enemyPrefab;
    [SerializeField]
    private int maxNumberOfSpawnersActive = 5;
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private List<Transform> spawnPoints;
    [SerializeField]
    private float timePerSpawn = 2f;

    private List<Transform> nearestActiveSpawners;

    private float currentTimeToSpawn;

    public SpawnerEvent OnCreation = new SpawnerEvent();

    private void Start()
    {
        currentTimeToSpawn = timePerSpawn;
    }

    private List<Transform> GetNearestActiveSpawners()
    {
        List<Transform> nearestSpawnPoints = new List<Transform>();

        float nearestDistance = float.MaxValue;
        int nearestIndex = -1;

        for (int i = 0; i < maxNumberOfSpawnersActive; i++)
        {
            nearestDistance = float.MaxValue;

            for (int j = 0; j < spawnPoints.Count; j++)
            {
                float distance = Vector3.Distance(playerMovement.transform.position, spawnPoints[j].position);
                 
                if (distance < nearestDistance)
                {
                    if(!nearestSpawnPoints.Contains(spawnPoints[j]))
                    {
                        nearestDistance = distance;
                        nearestIndex = j;
                    }
                }
            }

            nearestSpawnPoints.Add(spawnPoints[nearestIndex]);
            nearestIndex = -1;
        }

        return nearestSpawnPoints;
    }

    private void Update()
    {
        currentTimeToSpawn -= Time.deltaTime;

        if(currentTimeToSpawn <= 0f)
        {
            nearestActiveSpawners = GetNearestActiveSpawners();
            Transform randomSpawnPoint = nearestActiveSpawners[Random.Range(0, nearestActiveSpawners.Count)];
            EnemyMovement clone = Instantiate(enemyPrefab, randomSpawnPoint.position, Quaternion.LookRotation(randomSpawnPoint.forward));
            OnCreation.Invoke(clone);
            currentTimeToSpawn = timePerSpawn;
        }
    }

    private void OnDrawGizmos()
    {
        if(nearestActiveSpawners != null && nearestActiveSpawners.Count > 0)
        {
            foreach (Transform t in nearestActiveSpawners)
            {
                Debug.DrawLine(t.position, playerMovement.transform.position, Color.red);
            }
        }
    }
}
