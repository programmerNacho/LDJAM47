using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    [SerializeField]
    private float heatPerShot = 1f;

    private GunCooldownSystem gunCooldownSystem;

    private void Start()
    {
        gunCooldownSystem = GetComponent<GunCooldownSystem>();
    }

    public override void OnPressedShoot(Vector3 globalLookDirection)
    {
        if(gunCooldownSystem.CurrentHeat + heatPerShot <= gunCooldownSystem.MaxHeat)
        {
            gunCooldownSystem.Heat(heatPerShot);

            Ray ray = new Ray(shootPoint.position, shootPoint.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layersObjectives, QueryTriggerInteraction.Collide))
            {
                OnHitObjective.Invoke(hit);
            }
            else
            {
                OnMiss.Invoke();
            }

            OnShot.Invoke();
        }
    }
}
