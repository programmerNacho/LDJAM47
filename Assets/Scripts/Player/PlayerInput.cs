using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public struct ButtonState
    {
        public bool pressed;
        public bool hold;
        public bool released;
    }

    private Vector2 movementInput;
    public Vector2 MovementInput 
    {
        get 
        {
            return movementInput;
        } 

        private set 
        {
            movementInput = value;
        } 
    }
    private Vector2 lookInput;
    public Vector2 LookInput
    {
        get 
        {
            return lookInput;
        } 

        private set 
        {
            lookInput = value;
        }
    }

    private ButtonState fireButtonState;

    public ButtonState FireButtonState
    {
        get
        {
            return fireButtonState;
        }

        private set
        {
            fireButtonState = value;
        }
    }

    private void Update()
    {
        movementInput.x = Input.GetAxis("Horizontal");
        movementInput.y = Input.GetAxis("Vertical");

        lookInput = Input.mousePosition;

        fireButtonState.pressed = Input.GetButtonDown("Fire1");
        fireButtonState.hold = Input.GetButton("Fire1");
        fireButtonState.released = Input.GetButtonUp("Fire1");
    }
}
