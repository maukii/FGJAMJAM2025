using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInputHandler))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField] float baseMovementSpeed = 5f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] Transform cameraTransform;
    [SerializeField] Animator anim;

    CharacterController characterController;
    PlayerInputHandler inputHandler;
    Melee melee;


    void Awake()
    {
        Instance = this;

        characterController = GetComponent<CharacterController>();
        inputHandler = GetComponent<PlayerInputHandler>();
        melee = GetComponent<Melee>();
    }

    void Update()
    {
        GroundPlayer();

        if (melee.IsAttacking) return;

        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * inputHandler.MoveInput.y + right * inputHandler.MoveInput.x).normalized;
        float movementSpeed = baseMovementSpeed + UpgradesHandler.Instance.GetUpgradeValue(UpgradeType.PlayerMovementSpeed);
        characterController.Move(moveDirection * movementSpeed * Time.deltaTime);

        anim.SetBool("IsMoving", inputHandler.MoveInput.magnitude != 0f);
    }

    void GroundPlayer()
    {
        var position = characterController.transform.position;
        position.y = 0;
        characterController.transform.position = position;
    }

    void HandleRotation()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            Vector3 targetPoint = hit.point;
            Vector3 lookDirection = targetPoint - transform.position;
            lookDirection.y = 0f;
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
