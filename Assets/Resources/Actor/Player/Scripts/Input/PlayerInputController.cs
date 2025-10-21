using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private PlayerInput inputController;

    private void Awake()
    {
        inputController = new PlayerInput();
        inputController.Enable();

        PlayerState state = GetComponent<PlayerState>();
        if (!state) return;
        if (!state.CanMove) return;

        //Move UP
        inputController.Player.MoveUp.started += gameObject.AddComponent<PlayerMoveUp>().Execute;
        inputController.Player.MoveUp.canceled += gameObject.AddComponent<PlayerStopUp>().Execute;

        //Move DOWN
        inputController.Player.MoveDown.started += gameObject.AddComponent<PlayerMoveDown>().Execute;
        inputController.Player.MoveDown.canceled += gameObject.AddComponent<PlayerStopDown>().Execute;

        //Move RIGHT
        inputController.Player.MoveRight.started += gameObject.AddComponent<PlayerMoveRight>().Execute;
        inputController.Player.MoveRight.canceled += gameObject.AddComponent<PlayerStopRight>().Execute;

        //Move LEFT
        inputController.Player.MoveLeft.started += gameObject.AddComponent<PlayerMoveLeft>().Execute;
        inputController.Player.MoveLeft.canceled += gameObject.AddComponent<PlayerStopLeft>().Execute;

        //ATTACK
        inputController.Player.Attack.started += gameObject.AddComponent<PlayerAttack>().Execute;

        //FLOWERING
        inputController.Player.Flowering.started += gameObject.AddComponent<PlayerFlowering>().Execute;
        inputController.Player.Flowering.canceled += gameObject.GetComponent<PlayerFlowering>().Stop;

    }

    private void Update()
    {
        MovementController movement = GetComponent<MovementController>();
        if (!movement) return;

        if (!movement.MoveUp && inputController.Player.MoveUp.IsPressed())
        {
            PlayerMoveUp e = GetComponent<PlayerMoveUp>();

            //TODO: Mejorar esto... no sé si vale la pena un cb vacío
            InputAction.CallbackContext cb = new();
            if (e) e.Execute(cb);
        }
        else if (!movement.MoveDown && inputController.Player.MoveDown.IsPressed())
        {
            PlayerMoveDown e = GetComponent<PlayerMoveDown>();
            InputAction.CallbackContext cb = new();
            if (e) e.Execute(cb);
        }
    }
}