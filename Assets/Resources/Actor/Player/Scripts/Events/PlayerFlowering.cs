using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerFlowering : Event
{
    protected AnimationController animationController;
    protected PlayerState playerState;

    private void Awake()
    {
        animationController = GetComponent<AnimationController>();
        playerState = GetComponent<PlayerState>();
    }

    private string GetFlowerColour(EFlowerColours colour)
    {
        return colour switch
        {
            EFlowerColours.PINK => "pink",
            _ => "idle",
        };
    }

    public override void Execute(InputAction.CallbackContext context)
    {
        if (!playerState.IsOnFlower) return;

        Debug.Log($"Player Flowering Event. Colour: {GetFlowerColour(playerState.FlowerColour)}");

        playerState.Flowering();
        animationController.StartAnimation($"Player_{GetFlowerColour(playerState.FlowerColour)}", true, playerState.EndFlowering);
    }

    public override void Stop(InputAction.CallbackContext context)
    {
        if(!playerState.IsOnFlower) return;

        playerState.StopFlowering();
        animationController.StartAnimation("Player_idle");
    }
}