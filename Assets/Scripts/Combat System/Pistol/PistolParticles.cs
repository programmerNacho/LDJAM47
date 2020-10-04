using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolParticles : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem shotParticles;
    [SerializeField]
    private ParticleSystem bloodParticlePrefab;
    [SerializeField]
    private ParticleSystem stoneParticlePrefab;

    private Pistol pistol;

    private void Start()
    {
        pistol = GetComponent<Pistol>();
        pistol.OnHitObjective.AddListener(OnHit);
        pistol.OnMiss.AddListener(OnMiss);
    }

    private void OnHit(RaycastHit hit)
    {
        shotParticles.Play();
        if(hit.transform.tag == "Flesh")
        {
            ParticleSystem blood = Instantiate(bloodParticlePrefab, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(blood.gameObject, blood.main.duration);
        }
        else if(hit.transform.tag == "Stone")
        {
            ParticleSystem stone = Instantiate(stoneParticlePrefab, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(stone.gameObject, stone.main.duration);
        }
    }

    private void OnMiss()
    {
        shotParticles.Play();
    }
}
