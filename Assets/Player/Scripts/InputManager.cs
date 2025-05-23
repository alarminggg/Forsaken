using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    AnimatorManager animatorManager;
    PlayerLocomotion playerLocomotion;

    public Vector2 movementInput;
    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    public Vector2 cameraInput;
    public float cameraInputX;
    public float cameraInputY;

    public bool sprint_input;
    public bool jump_input;
    public bool attack_input;
    public bool pause_input;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            playerControls.PlayerActions.Sprint.performed += i => sprint_input = true;
            playerControls.PlayerActions.Sprint.canceled += i => sprint_input = false;

            playerControls.PlayerActions.Jump.performed += i => jump_input = true;

            playerControls.PlayerActions.Attack.performed += i => attack_input = true;

            playerControls.PlayerUIControl.PauseGame.performed += i => pause_input = true;
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleJumpInput();
        HandleAttackInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, playerLocomotion.isSprinting);
    }

    private void HandleSprintingInput()
    {
        if (sprint_input && moveAmount > 0.5f)
        {
            playerLocomotion.isSprinting = true;
        }
        else
        {
            playerLocomotion.isSprinting = false;
        }
    }

    private void HandleJumpInput()
    {
        if(jump_input)
        {
            jump_input = false;
            playerLocomotion.HandleJumping();
        }
    }

    private void HandleAttackInput()
    {
        if(attack_input)
        {
            Debug.Log("HandleAttackInput: attack_input is TRUE");
            playerLocomotion.HandleAttack();
            attack_input = false;
        }
    }

    
}
