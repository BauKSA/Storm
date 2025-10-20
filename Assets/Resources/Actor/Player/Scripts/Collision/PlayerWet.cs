using UnityEngine;

class PlayerWet
{
    public static void Wet(Collider2D player)
    {
        PlayerState state = player.gameObject.GetComponent<PlayerState>();
        if (!state) return;

        state.Wet();

        MovementController movement = player.GetComponent<MovementController>();
        if (movement) movement.Stop();

        AnimationController animation = player.GetComponent<AnimationController>();
        animation.StartAnimation("Player_wet", true, () =>
        {
            state.Dry();
            animation.StartAnimation("Player_idle");
        });
    }
}