using UnityEngine;

public class Health : MonoBehaviour
{
    private int Life = 100;

    public void Damage(int d = 10) { Life -= d; }
    public void Heal(int h = 15) { Life += h; }

    public int GetHealth() { return Life; }
}
