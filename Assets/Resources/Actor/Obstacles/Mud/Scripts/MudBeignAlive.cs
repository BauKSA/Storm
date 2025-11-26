using UnityEngine;

public class MudBeignAlive : MonoBehaviour
{
    private float currentTime = 0f;
    private readonly float destroyTime = 12f;

    private readonly float radio = 0.1f;

    // Fixed property declaration for Ant
    public GameObject Ant { get; set; }

    private void Start()
    {
        SpawnAround();
    }
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= destroyTime)
        {
            Destroy(gameObject);
        }
    }

    void SpawnAround()
    {
        int ants = Game.Instance.AntCountLimit;

        for (int i = 0; i < ants; i++)
        {
            float angle = i * Mathf.PI * 2f / ants;
            Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radio;

            Instantiate(Ant, (Vector2)transform.position + offset, Quaternion.identity);
        }
    }
}
