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
}