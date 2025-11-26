using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private MenuInput inputController;

    private void RestartGame(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        inputController.Disable();
        SceneManager.LoadScene("Menu");
    }

    void Awake()
    {
        inputController = new MenuInput();
        inputController.Enable();

        inputController.Menu.Start.started += RestartGame;
    }

    private void Start()
    {
        Cursor.visible = false;
    }
}
