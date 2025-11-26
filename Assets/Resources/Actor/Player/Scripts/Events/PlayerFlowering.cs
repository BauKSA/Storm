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
            EFlowerColours.PURPLE => "purple",
            EFlowerColours.LIGTHBLUE => "lightblue",
            EFlowerColours.YELLOW => "yellow",
            EFlowerColours.ORANGE => "orange",
            _ => "idle",
        };
    }

    private void StartFlowering()
    {
        if (!playerState.IsOnFlower) return;

        Debug.Log($"Player Flowering Event. Colour: {GetFlowerColour(playerState.FlowerColour)}");

        playerState.Flowering();
        animationController.StartAnimation($"Player_{GetFlowerColour(playerState.FlowerColour)}", true, playerState.EndFlowering);
    }

    private void StopFlowering()
    {
        if (!playerState.IsOnFlower) return;

        playerState.StopFlowering();
        animationController.StartAnimation("Player_idle");
    }

    private void StartSeed()
    {

        if (!playerState) return;
        if (!playerState.IsOnMud || !playerState.CurrentMud) return;
        if(Game.Instance.Seeds <= 0) return;

        Instantiate(Game.Instance.Seed, playerState.CurrentMud.transform.position, playerState.CurrentMud.transform.rotation);
        Destroy(playerState.CurrentMud);

        Game.Instance.Seeds -= 1;
    }

    public override void Execute(InputAction.CallbackContext context)
    {
        if (playerState.IsOnFlower) StartFlowering();
        else if (playerState.IsOnMud) StartSeed();
    }

    public override void Stop(InputAction.CallbackContext context)
    {
        if (playerState.IsOnFlower) StopFlowering();
    }
}