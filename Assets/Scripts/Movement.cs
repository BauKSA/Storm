using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool MoveRight { get; private set; } = false;
    public bool MoveUp { get; private set; } = false;
    public bool MoveDown { get; private set; } = false;
    public bool MoveLeft { get; private set; } = false;

    [SerializeField] private float _speed = 1f;

    private void Update()
    {
        Vector2 vector = Vector2.zero;
 

        if (MoveUp) vector.y = 1f;
        else if (MoveDown) vector.y = -1f;

        if (MoveRight) vector.x = 1f;
        else if (MoveLeft) vector.x = -1f;

        if (vector == Vector2.zero) return;

        vector.Normalize();
        vector *= _speed;
        
        PositionController positionController = GetComponent<PositionController>();
        if (!positionController) return;

        positionController.Move(vector * Time.deltaTime);
    }

    public void SetMoveUp(bool movement = true) {  MoveUp = movement; }
    public void SetMoveDown(bool movement = true) { MoveDown = movement; }
    public void SetMoveLeft(bool movement = true) { MoveLeft = movement; }
    public void SetMoveRight(bool movement = true) { MoveRight = movement; }
}
