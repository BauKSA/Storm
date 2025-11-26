using UnityEngine;

public enum EFlowerColours {
    NONE,
    LIGTHBLUE,
    YELLOW,
    PURPLE,
    PINK,
    ORANGE
}

public enum EFlowerState {
    GROWING,
    FLOWER,
    DAMAGED
}

public class FlowerState : MonoBehaviour
{
    public EFlowerColours Colour { get; private set; } = EFlowerColours.NONE;
    public EFlowerState State { get; private set; } = EFlowerState.GROWING;

    public void SelectColour() {
        if(Colour != EFlowerColours.NONE) return;

        int randomColour = Random.Range(1, 6);
        Colour = (EFlowerColours)randomColour;

        State = EFlowerState.FLOWER;
    }

    public void EndFlowering()
    {
        Destroy(gameObject);
    }

    public void Damage()
    {
        State = EFlowerState.DAMAGED;
    }

    public void StopDamage()
    {
        State = EFlowerState.FLOWER;
    }
}
