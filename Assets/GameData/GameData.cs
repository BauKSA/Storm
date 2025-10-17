using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }

    private int Points = 0;
    private int Level = 0;
    private float Speed = 0;

    //TEMP VARIABLES
    private readonly int PointsPerLevel = 50;
    private readonly float SpeedIncrementPerLevel = 0.2f;

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
            Speed += SpeedIncrementPerLevel;
        }
    }

    //GETTERS
    public int GetPoints(){ return Points; }
    public int GetLevel(){ return Level; }
    public float GetSpeed() { return Speed; }
}