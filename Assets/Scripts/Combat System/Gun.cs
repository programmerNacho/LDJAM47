using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class HitObjective : UnityEvent<RaycastHit> { }

public abstract class Gun : MonoBehaviour
{
    [SerializeField]
    protected Transform shootPoint;
    [SerializeField]
    protected LayerMask layersObjectives;

    public UnityEvent OnShot = new UnityEvent();
    public HitObjective OnHitObjective = new HitObjective();
    public UnityEvent OnMiss = new UnityEvent();

    public virtual void OnPressedShoot(Vector3 globalLookDirection) { }
    public virtual void OnHoldShoot(Vector3 globalLookDirection) { }
    public virtual void OnReleasedShoot(Vector3 globalLookDirection) { }
}
