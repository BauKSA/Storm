using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour
{
    private MenuInput inputController;

    [SerializeField] private TMP_Text uiText;
    [SerializeField] private Image titleImage;

    private void StartGame(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        titleImage.enabled = false;
        uiText.text = "Loading...";

        inputController.Disable();
        StartCoroutine(LoadLevelAsync());
    }

    private void QuitGame(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Application.Quit();
    }

    IEnumerator LoadLevelAsync()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync("Level");
        load.allowSceneActivation = false;
        if (load == null)
        {
            Debug.LogError("❌ ERROR: La escena 'Level' NO existe.");
            yield break;
        }

        uiText.text = "Loading...";

        while (!load.isDone)
        {
            // Mostrar progreso real
            float progress = Mathf.Clamp01(load.progress / 0.9f);
            uiText.text = $"Loading... {progress * 100f:0}%";

            Debug.Log($"progress={load.progress}  mapped={progress}");

            // Cuando llega al 90%
            if (load.progress >= 0.9f)
            {
                uiText.text = "Starting!";
                yield return new WaitForSeconds(0.4f);
                load.allowSceneActivation = true;
            }

            yield return null;
        }

        yield return new WaitForSeconds(0.4f);

        load.allowSceneActivation = true;
    }

    void Awake()
    {
        inputController = new MenuInput();
        inputController.Enable();

        inputController.Menu.Start.started += StartGame;
        inputController.Menu.Quit.started += QuitGame;
    }

    private void Start()
    {
        Cursor.visible = false;
    }
}
