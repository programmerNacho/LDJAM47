using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoul : MonoBehaviour
{
    [SerializeField]
    private int soulIntensity = 1;

    private Health health;

    private void Start()
    {
        health = GetComponent<Health>();
        health.OnDie.AddListener(SoulRelease);
    }

    private void OnDestroy()
    {
        health.OnDie.RemoveListener(SoulRelease);
    }

    private void SoulRelease()
    {
        FindObjectOfType<SoulSystem>().ZombieKilled(transform.position, soulIntensity);
    }
}
