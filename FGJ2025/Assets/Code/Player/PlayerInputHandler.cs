using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public bool BubbleMelee { get; private set; }
    public bool StabMelee { get; private set; }


    public void OnMove(InputAction.CallbackContext context) => MoveInput = context.ReadValue<Vector2>();

    public void OnBubbleMelee(InputAction.CallbackContext context) => BubbleMelee = context.started || context.performed;

    public void OnStabMelee(InputAction.CallbackContext context) => StabMelee = context.started || context.performed;
}
