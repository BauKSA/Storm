using UnityEngine.InputSystem;

public class PlayerMoveDown : Event
{
    protected MovementController playerMovement;
    protected PlayerState playerState;

    private void Awake()
    {
        playerMovement = GetComponent<MovementController>();
        playerState = GetComponent<PlayerState>();
    }

    public override void Execute(InputAction.CallbackContext context)
    {
        if (!playerMovement) return;
        if (!playerState.CanMove) return;

        playerMovement.SetMoveDown();
    }
}
