using UnityEngine;

public class AntState : MonoBehaviour
{
    public bool IsEating { get; private set; } = false;

    public void StartEating()
    {
        IsEating = true;
    }

    public void StopEating()
    {
        IsEating = false;
    }
}
