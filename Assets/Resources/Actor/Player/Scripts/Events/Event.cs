using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Event : MonoBehaviour
{
    public abstract void Execute(InputAction.CallbackContext context);
}