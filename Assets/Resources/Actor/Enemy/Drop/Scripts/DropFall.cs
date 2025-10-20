using UnityEngine;

public class DropFall : MonoBehaviour
{
    MovementController movement;
    void Awake()
    {
        movement = GetComponent<MovementController>();
    }

    private void Start()
    {
        movement.SetMoveDown();
    }
}
