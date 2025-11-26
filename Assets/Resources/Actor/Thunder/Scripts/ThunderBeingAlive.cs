using UnityEngine;

public class ThunderBeingAlive : MonoBehaviour
{
    private SpriteRenderer sprRenderer;
    private PolygonCollider2D polygon;
    private ThunderState state;

    private string lastSprite = "";

    void Awake()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        polygon = GetComponent<PolygonCollider2D>();
        state = GetComponent<ThunderState>();
    }

    void Death()
    {
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AnimationController controller = GetComponent<AnimationController>();
        if (!controller) return;

        controller.StartAnimation("Thunder_idle", true, Death);
        lastSprite = sprRenderer.sprite.name;

        if (!sprRenderer) return;

        sprRenderer.sortingLayerName = "Thunder";

        Destroy(polygon);
        polygon = gameObject.AddComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state.ChangingCollider) state.StopChangingCollider();

        if (lastSprite != sprRenderer.sprite.name)
        {
            state.ChangeCollider();
            lastSprite = sprRenderer.sprite.name;
            Destroy(polygon);
            polygon = gameObject.AddComponent<PolygonCollider2D>();
        }
    }
}
