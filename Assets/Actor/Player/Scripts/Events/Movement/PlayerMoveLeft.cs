using UnityEngine.InputSystem;

public class PlayerMoveLeft : Event
{
    protected Movement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<Movement>();
    }

    public override void Execute(InputAction.CallbackContext context)
    {
        if (!playerMovement) return;
        playerMovement.SetMoveLeft();
    }
}
