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
            if (!playerState.IsVulnerable) return;

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

        if(other.CompareTag("mud"))
        {
            Debug.Log("Entered Mud");
            playerState.OnMud(other.gameObject);
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
                Game.Instance.Seeds += 1;

                if(Game.Instance.Seeds > Game.Instance.MaxSeeds)
                {
                    Game.Instance.Seeds = Game.Instance.MaxSeeds;
                }
            }
        }

        if(other.CompareTag("flower"))
        {
            if (playerState.IsFlowering) return;

            FlowerState flowerState = other.gameObject.GetComponent<FlowerState>();
            if (!flowerState) return;


            if (flowerState.State != EFlowerState.FLOWER) return;

            PlayerOnFlower.OnFlower(gameObject, other.gameObject);
        }


        if (other.CompareTag("mud") && !playerState.IsOnMud)
        {
            Debug.Log("Staying in Mud");
            playerState.OnMud(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ant"))
        {
            playerState.StopDamage();
            animationController.SetNextAnimation("Player_idle");
        }

        if (other.CompareTag("flower"))
        {
            PlayerOnFlower.LeftFlower(gameObject);
            animationController.SetNextAnimation("Player_idle");
        }


        if (other.CompareTag("mud"))
        {
            Debug.Log("Left Mud");
            playerState.LeftMud();
        }
    }
}
