using UnityEngine;

public class TreasureManager : MonoBehaviour
{
    [Header("Treasures")]
    public Treasure[] treasures;

    private void Start()
    {
        if (treasures != null && treasures.Length > 0)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.totalTreasures = treasures.Length;
            }
        }
        else
        {
            Debug.LogWarning("TreasureManager: El arreglo 'treasures' está vacío. GameManager usará el valor por defecto (" + (GameManager.Instance != null ? GameManager.Instance.totalTreasures : 10) + ").");
        }

        Debug.Log("Tesoros registrados en TreasureManager: " + (treasures != null ? treasures.Length : 0));
    }
}