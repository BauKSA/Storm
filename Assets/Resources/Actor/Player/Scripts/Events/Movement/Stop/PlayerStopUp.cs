using UnityEngine.InputSystem;

public class PlayerStopUp : Event
{
    protected MovementController playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<MovementController>();
    }

    public override void Execute(InputAction.CallbackContext context)
    {
        if (!playerMovement) return;
        playerMovement.SetMoveUp(false);
    }
}
