using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolDamager : Damager
{
    private Pistol pistol;

    private void Start()
    {
        pistol = GetComponent<Pistol>();
        pistol.OnHitObjective.AddListener(DamageObjectiveHealth);
    }

    private void OnDestroy()
    {
        pistol.OnHitObjective.RemoveListener(DamageObjectiveHealth);
    }

    private void DamageObjectiveHealth(RaycastHit hit)
    {
        Health objectivesHealth = hit.transform.GetComponent<Health>();
        if(objectivesHealth)
        {
            base.Damage(objectivesHealth);
        }
    }
}
