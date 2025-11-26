using UnityEngine;

public class AntCollision : MonoBehaviour
{
    private string GetFlowerColour(EFlowerColours colour)
    {
        return colour switch
        {
            EFlowerColours.PINK => "pink",
            EFlowerColours.PURPLE => "purple",
            EFlowerColours.LIGTHBLUE => "lightblue",
            EFlowerColours.YELLOW => "yellow",
            EFlowerColours.ORANGE => "orange",
            _ => "idle",
        };
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("flower")) return;

        FlowerState flowerState = other.gameObject.GetComponent<FlowerState>();
        if (!flowerState) return;

        if(flowerState.State != EFlowerState.FLOWER) return;

        flowerState.Damage();

        AnimationController flowerAnimation = other.gameObject.GetComponent<AnimationController>();
        if (!flowerAnimation) return;

        flowerAnimation.StartAnimation("Flower_damage");

        AnimationController antAnimation = GetComponent<AnimationController>();
        if(!antAnimation) return;

        antAnimation.StartAnimation("Ant_eating");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("flower")) return;

        FlowerState flowerState = other.gameObject.GetComponent<FlowerState>();
        if (!flowerState) return;

        flowerState.StopDamage();

        AnimationController flowerAnimation = other.gameObject.GetComponent<AnimationController>();
        if (!flowerAnimation) return;

        flowerAnimation.StartAnimation($"Flower_{GetFlowerColour(flowerState.Colour)}");

        AnimationController antAnimation = GetComponent<AnimationController>();
        if (!antAnimation) return;

        antAnimation.StartAnimation("Ant_idle");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("flower"))
        {

            FlowerState flowerState = other.gameObject.GetComponent<FlowerState>();
            if (!flowerState) return;

            if (flowerState.State != EFlowerState.FLOWER) return;

            flowerState.Damage();

            AnimationController flowerAnimation = other.gameObject.GetComponent<AnimationController>();
            if (!flowerAnimation) return;

            flowerAnimation.StartAnimation("Flower_damage");

            AnimationController antAnimation = GetComponent<AnimationController>();
            if (!antAnimation) return;

            antAnimation.StartAnimation("Ant_eating");
        }
        else if (other.CompareTag("player"))
        {
            PlayerState playerState = other.GetComponent<PlayerState>();
            if (!playerState) return;
            if (!playerState.IsVulnerable) return;

            Game.Instance.Health -= Game.Instance.AntDamage;
        }
    }
}
