using UnityEngine.InputSystem;

public class PlayerStopDown : Event
{
    protected MovementController playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<MovementController>();
    }

    public override void Execute(InputAction.CallbackContext context)
    {
        if (!playerMovement) return;
        playerMovement.SetMoveDown(false);
    }
}
