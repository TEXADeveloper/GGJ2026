using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private InputActionAsset inputAsset;
    private InputActionMap actionMap;
    private InputAction moveAction;
    private InputAction lookAction;

    PlayerMovement pM;
    PlayerFlashLight pFL;
    PlayerInteraction pI;

    bool paused = false;

    void Awake()
    {
        PlayerInput playerInput = this.GetComponent<PlayerInput>();
        inputAsset = playerInput.actions;
        actionMap = inputAsset.FindActionMap("Default");

        moveAction = actionMap.FindAction("Movement");
        lookAction = actionMap.FindAction("Look");
    }

    void OnEnable()
    {
        actionMap.FindAction("Sprint").performed += sprint;
        actionMap.FindAction("Sprint").canceled += sprint;
        actionMap.FindAction("Flashlight").performed += flashlight;
        actionMap.FindAction("Interact").performed += interact;
        actionMap.FindAction("Pause").performed += pause;
    }

    void OnDisable()
    {
        actionMap.FindAction("Sprint").performed -= sprint;
        actionMap.FindAction("Sprint").canceled -= sprint;
        actionMap.FindAction("Flashlight").performed -= flashlight;
        actionMap.FindAction("Interact").performed -= interact;
        actionMap.FindAction("Pause").performed -= pause;
    }

    void Start()
    {
        pM = this.GetComponent<PlayerMovement>();
        pFL = this.GetComponent<PlayerFlashLight>();
        pI = this.GetComponent<PlayerInteraction>();
    }

    void Update()
    {
        movementInput();
        lookInput();
    }

    private void movementInput()
    {
        if (!paused)
            pM.SetMoveInput(moveAction.ReadValue<Vector2>());
    }

    private void lookInput()
    {
        if (!paused)
            pM.SetLookInput(lookAction.ReadValue<Vector2>());
    }

    public void sprint(InputAction.CallbackContext ctx)
    {
        //sprint
        Debug.Log("Sprint");
    }
    
    public void flashlight(InputAction.CallbackContext ctx)
    {
        pFL.Toggle();
    }

    public void interact(InputAction.CallbackContext ctx)
    {
        pI.Interact();
    }

    public void pause(InputAction.CallbackContext ctx)
    {
        //pause
        Debug.Log("Pause");
    }
}
