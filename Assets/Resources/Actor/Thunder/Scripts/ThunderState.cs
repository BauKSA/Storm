using UnityEngine;

public class ThunderState : MonoBehaviour
{
    public bool ChangingCollider { get; private set; } = false;
    public bool Damage { get; private set; } = true;

    public void ChangeCollider()
    {
        ChangingCollider = true;
    }

    public void StopChangingCollider()
    {
        ChangingCollider = false;
    }

    public void NoDamage()
    {
        Damage = false;
    }
}
