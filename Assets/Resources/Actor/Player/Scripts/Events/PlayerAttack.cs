using UnityEngine.InputSystem;

public class PlayerAttack : Event
{
    protected AnimationController animationController;
    protected PlayerState playerState;

    private void Awake()
    {
        animationController = GetComponent<AnimationController>();
        playerState = GetComponent<PlayerState>();
    }

    public override void Execute(InputAction.CallbackContext context)
    {
        playerState.Attack();
        animationController.StartAnimation("Player_attack", true, playerState.StopAttack);
    }
}