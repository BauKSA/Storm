using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private PlayerInput inputController;

    private void Awake()
    {
        inputController = new PlayerInput();
        inputController.Enable();

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
    }
}