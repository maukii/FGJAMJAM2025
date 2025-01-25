using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public bool IsShooting { get; private set; }


    public void OnMove(InputAction.CallbackContext context) => MoveInput = context.ReadValue<Vector2>();

    public void OnShoot(InputAction.CallbackContext context) => IsShooting = context.started || context.performed;
}
