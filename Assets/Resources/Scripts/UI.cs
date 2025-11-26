using TMPro;
using UnityEngine;

public class PointsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text UIText;

    private void Update()
    {
        UIText.text =
            $"{Game.Instance.Points:0000}   " +
            $"{Game.Instance.Level:0000}   " +
            $"{Game.Instance.Seeds:0}/{Game.Instance.MaxSeeds:0}   :   " +
            $"{Game.Instance.Health:000.000}/100.000   " + 
            $"{Game.Instance.Lives}";
    }
}