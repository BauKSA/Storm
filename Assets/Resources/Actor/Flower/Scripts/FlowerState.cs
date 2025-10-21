using UnityEngine;

public enum EFlowerState
{
    SPROUT,
    SMALL_STEM,
    LEAF,
    LONG_STEM,
    BLOOMING,
    FLOWER
};

public enum EFlowerColours
{
    NONE,
    PINK
}

public class FlowerState : MonoBehaviour
{
    public EFlowerState State { get; private set; } = EFlowerState.SPROUT;
    public EFlowerColours Colour {  get; private set; } = EFlowerColours.NONE;

    private float nextStateDelay = 2f;
    private float currentTime = 0;

    private void Awake()
    {
        //TODO: Traer un color random
        Colour = EFlowerColours.PINK;
    }

    void Update()
    {
        if (State == EFlowerState.FLOWER) return;

        currentTime += Time.deltaTime;
        if (currentTime >= nextStateDelay)
        {
            currentTime -= nextStateDelay;
            State = NextState();
            Debug.Log($"Next state: {State}");
        }
    }

    private EFlowerState NextState()
    {

        //TODO: Seguramente se pueda mejorar un poco esto... pero por ahora lo dejo así
        AnimationController animationController = GetComponent<AnimationController>();
        if (!animationController) return EFlowerState.FLOWER;

        switch (State)
        {
            default:
                animationController.StartAnimation("Flower_flower");
                return EFlowerState.FLOWER;
            case EFlowerState.SPROUT:
                animationController.StartAnimation("Flower_smallstem");
                return EFlowerState.SMALL_STEM;
            case EFlowerState.SMALL_STEM:
                animationController.StartAnimation("Flower_leaf");
                return EFlowerState.LEAF;
            case EFlowerState.LEAF:
                animationController.StartAnimation("Flower_longstem");
                return EFlowerState.LONG_STEM;
            case EFlowerState.LONG_STEM:
                animationController.StartAnimation("Flower_blooming");
                return EFlowerState.BLOOMING;
            case EFlowerState.BLOOMING:
                animationController.StartAnimation("Flower_pinkflower");
                return EFlowerState.FLOWER;
            case EFlowerState.FLOWER:
                return EFlowerState.FLOWER;
        }
    }

    public void Reset()
    {
        //TODO: Mejorar el sistema de aumento de delay
        State = EFlowerState.BLOOMING;
        nextStateDelay += 1f;

        AnimationController animationController = GetComponent<AnimationController>();
        animationController.StartAnimation("Flower_blooming");
    }
}
