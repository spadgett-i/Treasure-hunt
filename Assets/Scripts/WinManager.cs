
using UnityEngine;

public class WinManager : MonoBehaviour
{
    [Header("Victory UI")]
    public GameObject winPanel;

    private bool gameWon = false;

    public void ShowWinScreen()
    {
        if (gameWon)
            return;

        gameWon = true;

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        Debug.Log("¡TREASURE HUNT COMPLETADO!");
    }

    public void HideWinScreen()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
    }
}