using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AntBeingAlive : MonoBehaviour
{
    private readonly string targetTag = "flower";
    public Transform closestTarget;
    private float speed = 0.15f;

    public float extraDistance = 1f;

    private Vector3 startPos;
    private Vector3 endPos;

    public Transform GetClosestTarget(bool withPlayer = true)
    {
        List<GameObject> list = GameObject.FindGameObjectsWithTag(targetTag).ToList();
        if (withPlayer) list.Add(Game.Instance.Player);

        GameObject[] targets = list.ToArray();

        Transform closest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject t in targets)
        {
            if (!t) continue;

            float dist = Vector3.Distance(transform.position, t.transform.position);

            if (dist < minDist)
            {
                minDist = dist;
                closest = t.transform;
            }
        }

        return closest;
    }

    void Start()
    {
        closestTarget = GetClosestTarget();

        SpriteRenderer sprRenderer = GetComponent<SpriteRenderer>();
        if (!sprRenderer) return;

        sprRenderer.sortingLayerName = "Ant";

        Vector2 range = new(0.1f, 0.3f);
        if (closestTarget.CompareTag("Player"))
        {
            range.x = 0.5f;
            range.y = 1f;
        }

        speed = Random.Range(range.x, range.y);
    }

    void Update()
    {
        if (!closestTarget)
            closestTarget = GetClosestTarget();

        transform.position = Vector3.MoveTowards(
            transform.position,
            closestTarget.position,
            speed * Time.deltaTime
        );
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            GetClosestTarget();
        }
    }
}
