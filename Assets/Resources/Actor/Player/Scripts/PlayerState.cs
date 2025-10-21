using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState : MonoBehaviour
{
    //GENERAL
    public bool IsVulnerable { get; private set; } = true;
    public bool CanMove { get; private set; } = true;

    //ANT
    public bool IsAttacking { get; private set; } = false;
    public bool IsBeingDamaged { get; private set; } = false;

    //DROP
    public bool IsWet {  get; private set; } = false;

    //FLOWERING
    public bool IsFlowering { get; private set; } = false;
    public bool IsOnFlower { get; private set; } = false;
    public EFlowerColours FlowerColour { get; private set; } = EFlowerColours.NONE;
    public GameObject CurrentFlowering { get; private set; } = null;

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

    public void OnFlower(GameObject flower)
    {
        FlowerState flowerState = flower.GetComponent<FlowerState>();
        if (!flowerState) return;

        FlowerColour = flowerState.Colour;
        IsOnFlower = true;
        CurrentFlowering = flower;
    }

    public void LeftFlower()
    {
        IsOnFlower = false;
    }

    public void Flowering()
    {
        IsFlowering = true;
        CanMove = false;
    }

    public void StopFlowering()
    {
        IsFlowering = false;
        CanMove = true;
    }

    public void EndFlowering()
    {
        FlowerState flowerState = CurrentFlowering.GetComponent<FlowerState>();
        if (!flowerState) return;

        flowerState.Reset();
        StopFlowering();
    }

    public void Damage() { IsBeingDamaged = true; }
    public void StopDamage() { IsBeingDamaged = false; }
}
