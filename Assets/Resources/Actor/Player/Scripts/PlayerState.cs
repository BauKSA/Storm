using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState : MonoBehaviour
{
    //GENERAL
    public bool IsVulnerable { get; private set; } = true;
    public bool CanMove { get; private set; } = true;
    public bool CanAttack { get; private set; } = true;

    //MUD
    public bool IsOnMud { get; private set; } = false;
    public GameObject CurrentMud { get; private set; } = null;

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
        CanAttack = false;
        IsAttacking = true;
    }
    public void StopAttack() {
        CanAttack = true;
        IsAttacking = false;
    }

    public void Wet()
    {
        CanAttack = false;
        IsWet = true;
        IsVulnerable = false;
        CanMove = false;
    }

    public void Dry()
    {
        CanAttack = true;
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
        CanAttack = false;
        IsFlowering = true;
        CanMove = false;
    }

    public void OnMud(GameObject mud)
    {
        IsOnMud = true;
        CurrentMud = mud;
    }

    public void LeftMud()
    {
        IsOnMud = false;
        CurrentMud = null;
    }

    public void StopFlowering()
    {
        CanAttack = true;
        IsFlowering = false;
        CanMove = true;
    }


    public void EndFlowering()
    {
        FlowerState flowerState = CurrentFlowering.GetComponent<FlowerState>();
        if (!flowerState) return;

        flowerState.EndFlowering();
        StopFlowering();

        Game.Instance.SumPoints(10);
        Game.Instance.Health += 1f;

        if(Game.Instance.Health > 100f)
        {
            Game.Instance.Health = 100f;
        }
    }

    public void Damage() { Debug.Log("damage"); IsBeingDamaged = true; }
    public void StopDamage() { Debug.Log("stop damage"); IsBeingDamaged = false; }

    public void Death()
    {
        CanAttack = false;
        CanMove = false;
        IsVulnerable = false;
    }
    public void Respawn()
    {
        CanAttack = true;
        CanMove = true;
        IsVulnerable = true;
    }
}
