using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool IsAttacking { get; private set; } = false;
    public bool IsBeingDamaged { get; private set; } = false;

    public void Attack() {
        Debug.Log("Start attack");
        IsAttacking = true;
    }
    public void StopAttack() {
        Debug.Log("Stop attack");
        IsAttacking = false;
    }

    public void Damage() { IsBeingDamaged = true; }
    public void StopDamage() { IsBeingDamaged = false; }
}
