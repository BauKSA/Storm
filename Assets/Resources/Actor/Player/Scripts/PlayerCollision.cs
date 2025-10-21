using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerState playerState;
    private AnimationController animationController;

    private void Awake()
    {
        playerState = GetComponent<PlayerState>();
        animationController = GetComponent<AnimationController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ant") && !playerState.IsAttacking)
        {
            playerState.Damage();
            animationController.StartAnimation("Player_damage");
        }

        if (other.CompareTag("flower"))
        {
            FlowerState flowerState = other.gameObject.GetComponent<FlowerState>();
            if (!flowerState) return;

            if (flowerState.State != EFlowerState.FLOWER) return;

            PlayerOnFlower.OnFlower(gameObject, other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("ant"))
        {
            if (playerState.IsAttacking)
            {
                playerState.StopDamage();
                Destroy(other.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ant"))
        {
            playerState.StopDamage();
            animationController.SetNextAnimation("Player_idle");
        }
    }
}
