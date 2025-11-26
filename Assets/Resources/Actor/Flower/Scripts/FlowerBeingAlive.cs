using UnityEngine;

public class FlowerBeingAlive : MonoBehaviour
{
    FlowerState flowerState;
    private float damageTime = 0f;

    void Start()
    {
        AnimationController animation = GetComponent<AnimationController>();
        animation.StartAnimation("Flower_idle", true, GrowUp);

        SpriteRenderer sprRenderer = GetComponent<SpriteRenderer>();
        if (!sprRenderer) return;

        sprRenderer.sortingLayerName = "Flower";
        flowerState = GetComponent<FlowerState>();
    }

    private void Update()
    {
        if (!flowerState) return;

        if(flowerState.State == EFlowerState.DAMAGED)
        {
            damageTime += Time.deltaTime;
            if(damageTime >= Game.Instance.FlowerDeathTime)
            {
                Destroy(gameObject);
            }
        }
    }

    private void GrowUp()
    {
        FlowerState state = GetComponent<FlowerState>();
        if (!state) return;

        state.SelectColour();

        AnimationController animation = GetComponent<AnimationController>();

        switch (state.Colour)
        {
            case EFlowerColours.LIGTHBLUE:
                animation.StartAnimation("Flower_lightblue");
                break;
            case EFlowerColours.YELLOW:
                animation.StartAnimation("Flower_yellow");
                break;
            case EFlowerColours.PURPLE:
                animation.StartAnimation("Flower_purple");
                break;
            case EFlowerColours.PINK:
                animation.StartAnimation("Flower_pink");
                break;
            case EFlowerColours.ORANGE:
                animation.StartAnimation("Flower_orange");
                break;
            default:
                return;
        }
    }
}
