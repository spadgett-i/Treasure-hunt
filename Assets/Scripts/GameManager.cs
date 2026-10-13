using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Treasure Settings")]
    public int totalTreasures = 10;

    public WinManager winManager;

    public TreasureCounterUI treasureCounterUI;

    private int treasuresFound = 0;

    public int TreasuresFound => treasuresFound;

    private void Awake()
    {
        // Crear una única instancia del GameManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Debug.Log("Treasure Hunt iniciado.");
        Debug.Log("Tesoros: 0/" + totalTreasures);
    }

    public void TreasureCollected()
    {
        treasuresFound++;

        Debug.Log("Tesoro encontrado: " +
                  treasuresFound + "/" + totalTreasures);

        if (treasureCounterUI != null)
        {
            treasureCounterUI.RefreshCounter();
        }

        if (treasuresFound >= totalTreasures)
        {
            CompleteGame();
        }
    }

    private void CompleteGame()
    {
        Debug.Log("¡TREASURE HUNT COMPLETADO!");

        if (winManager != null)
        {
            winManager.ShowWinScreen();
        }
    }
}