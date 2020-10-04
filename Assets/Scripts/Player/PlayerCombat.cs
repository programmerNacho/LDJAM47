using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    private Gun currentGun;

    public Gun CurrentGun
    {
        get
        {
            return currentGun;
        }

        private set
        {
            currentGun = value;
        }
    }

    private PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if(currentGun)
        {
            PlayerInput.ButtonState fireButtonState = playerInput.FireButtonState;

            if (fireButtonState.pressed)
            {
                currentGun.OnPressedShoot(transform.forward);
            }
            else if (fireButtonState.hold)
            {
                currentGun.OnHoldShoot(transform.forward);
            }
            else if (fireButtonState.released)
            {
                currentGun.OnReleasedShoot(transform.forward);
            }
        }
    }
}
