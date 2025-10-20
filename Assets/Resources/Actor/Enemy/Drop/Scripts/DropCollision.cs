using UnityEngine;
using System.Collections;

public class DropCollision : MonoBehaviour
{
    private bool toDestroy = false;
    private bool pendingDestroy = false;
    private float verticalLimit = 0f;
    private bool fallen = false;

    private void Awake()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (!renderer) return;

        Sprite sprite = renderer.sprite;

        verticalLimit = Random.Range(-1 + sprite.rect.y, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (fallen) return;
        if (other.CompareTag("ant")) return;

        MovementController movementController = GetComponent<MovementController>();
        if(!movementController) return;

        movementController.SetMoveDown(false);

        if (other.CompareTag("player"))
        {
            PlayerWet.Wet(other);
            pendingDestroy = true;
        }
        else
        {
            AnimationController animation = GetComponent<AnimationController>();
            if(!animation) return;

            animation.StartAnimation("Drop_fall", true, SelfDestroy);
            fallen = true;
        }
    }

    private void Update()
    {
        if (toDestroy)
        {
            Destroy(gameObject);
            return;
        }

        if (pendingDestroy)
        {
            pendingDestroy = false;
            toDestroy = true;
        }

        PositionController position = GetComponent<PositionController>();
        if(!position) return;

        if(position.Position.y <= verticalLimit)
        {
            MovementController movementController = GetComponent<MovementController>();
            if (!movementController) return;

            movementController.SetMoveDown(false);

            AnimationController animation = GetComponent<AnimationController>();
            if (!animation) return;

            animation.StartAnimation("Drop_fall", true, SelfDestroy);
            animation.SetNextAnimation("Drop_fallen");
        }
    }

    public void SelfDestroy()
    {
        StartCoroutine(DestroyNextFrame());
    }

    private IEnumerator DestroyNextFrame()
    {
        yield return null;
        pendingDestroy = true;
    }
}
