using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    public override void OnPressedShoot(Vector3 globalLookDirection)
    {
        Ray ray = new Ray(shootPoint.position, shootPoint.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layersObjectives, QueryTriggerInteraction.Collide))
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
