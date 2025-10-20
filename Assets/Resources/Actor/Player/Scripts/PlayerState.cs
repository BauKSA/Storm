using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool IsVulnerable { get; private set; } = true;
    public bool CanMove { get; private set; } = true;
    public bool IsAttacking { get; private set; } = false;
    public bool IsBeingDamaged { get; private set; } = false;
    public bool IsWet {  get; private set; } = false;

    public void Attack() {
        Debug.Log("Start attack");
        IsAttacking = true;
    }
    public void StopAttack() {
        Debug.Log("Stop attack");
        IsAttacking = false;
    }

    public void Wet()
    {
        IsWet = true;
        IsVulnerable = false;
        CanMove = false;
    }

    public void Dry()
    {
        IsWet = false;
        IsVulnerable = true;
        CanMove = true;
    }

    public void Damage() { IsBeingDamaged = true; }
    public void StopDamage() { IsBeingDamaged = false; }
}
