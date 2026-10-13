using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [Header("UI Paneles")]
    public GameObject pauseMenuCanvas;
    public GameObject mainPanel;
    public GameObject optionsPanel;

    [Header("Posicionamiento VR")]
    public Transform headCamera;
    public float spawnDistance = 1.5f;

    [Header("Input de Pausa (Botón Menu/Start)")]
    public InputActionProperty pauseButtonAction;

    private bool isPaused = false;

    private void Start()
    {
        // Asegurar que el menú inicie oculto al arrancar el juego
        ResumeGame();
    }

    private void Update()
    {
        // Detectar si el jugador presiona el botón de menú en el mando
        if (pauseButtonAction.action != null && pauseButtonAction.action.WasPressedThisFrame())
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Detener el tiempo del juego

        // Posicionar el Canvas justo frente a la cabeza del jugador
        if (headCamera != null && pauseMenuCanvas != null)
        {
            pauseMenuCanvas.transform.position = headCamera.position + (headCamera.forward * spawnDistance);
            pauseMenuCanvas.transform.rotation = Quaternion.LookRotation(headCamera.forward);
            // Mantener la interfaz horizontal/nivelada
            pauseMenuCanvas.transform.eulerAngles = new Vector3(0, pauseMenuCanvas.transform.eulerAngles.y, 0);
        }

        if (pauseMenuCanvas != null) pauseMenuCanvas.SetActive(true);
        OpenMainPanel();
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Reanudar tiempo

        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(false);
        }
    }

    // --- Navegación entre Paneles ---

    public void OpenMainPanel()
    {
        if (mainPanel != null) mainPanel.SetActive(true);
        if (optionsPanel != null) optionsPanel.SetActive(false);
    }

    public void OpenOptionsPanel()
    {
        if (mainPanel != null) mainPanel.SetActive(false);
        if (optionsPanel != null) optionsPanel.SetActive(true);
    }

    // --- Control de Escenas ---

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        // Reemplaza "MainMenu" con el nombre exacto de tu escena de menú principal
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Cerrando aplicación...");
        Application.Quit();
    }
}