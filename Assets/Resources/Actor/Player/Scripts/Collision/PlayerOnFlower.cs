using UnityEngine;

class PlayerOnFlower
{
    public static void OnFlower(GameObject player, GameObject flower)
    {
        PlayerState state = player.GetComponent<PlayerState>();
        if (!state) return;

        Debug.Log("Player On Flower");
        state.OnFlower(flower);
    }

    public static void LeftFlower(GameObject player)
    {
        PlayerState state = player.GetComponent<PlayerState>();
        if (!state) return;

        Debug.Log("Player Off Flower");
        state.LeftFlower();
    }
}