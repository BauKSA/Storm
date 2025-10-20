using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    public int Points { get; private set; } = 0;
    public int Level { get; private set; } = 0;

    //Drop
    public float DropSpawnDelay { get; private set; } = 1.5f;
    public int DropCountLimit { get; private set; } = 1;

    //TEMP VARIABLES
    private readonly int PointsPerLevel = 50;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SumPoints(int p)
    {
        Points += p;
        if (Points % PointsPerLevel == 0)
        {
            Level++;
            DropSpawnDelay = Mathf.Max(0.6f, 1.5f - (Level * 0.1f));
            DropCountLimit = Mathf.Min(5, 1 + Level);
        }
    }
}