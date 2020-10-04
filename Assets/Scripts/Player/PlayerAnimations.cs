using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private PlayerMovement playerMovement;
    private PlayerCombat playerShooting;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponent<PlayerCombat>();

        OnChangeGun(playerShooting.CurrentGun);
    }

    private void OnChangeGun(Gun gun)
    {
        Pistol pistol = gun as Pistol;

        pistol.OnHitObjective.AddListener(PlayShootAnimation);
        pistol.OnMiss.AddListener(PlayShootAnimation);
    }

    private void PlayShootAnimation(RaycastHit hit)
    {
        PlayShootAnimation();
    }

    private void PlayShootAnimation()
    {
        animator.Play("PistolShooting", animator.GetLayerIndex("Shooting"), 0f);
    }

    private void Update()
    {
        animator.SetBool("Moving", playerMovement.MovementVelocity.magnitude > 0.2f);

        Vector3 localMoveDirection = transform.InverseTransformDirection(playerMovement.MovementVelocity.normalized);
        Vector3 localNormalizedMoveSpeed = localMoveDirection * (playerMovement.MovementVelocity.magnitude / playerMovement.MaxMovementSpeed);
        animator.SetFloat("X", localNormalizedMoveSpeed.x);
        animator.SetFloat("Y", localNormalizedMoveSpeed.z);
    }
}
