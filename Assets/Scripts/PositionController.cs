using UnityEngine;

public struct Position2D
{
    public float x;
    public float y;
}

public class PositionController : MonoBehaviour
{
    [SerializeField] private Position2D _position = new();
    public Position2D Position => _position;

    private void Awake()
    {
        _position.x = transform.position.x;
        _position.y = transform.position.y;
    }

    private void SyncPosition()
    {
        _position.x = transform.position.x;
        _position.y = transform.position.y;
    }

    public void Move(Vector2 vector)
    {
        transform.Translate(vector);
        SyncPosition();
    }
}
