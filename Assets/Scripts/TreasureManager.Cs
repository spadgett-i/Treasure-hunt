using UnityEngine;

public class TreasureManager : MonoBehaviour
{
    [Header("Treasures")]
    public Treasure[] treasures;

    private void Start()
    {
        // Si hay tesoros configurados,
        // actualizar automáticamente el total.
        if (treasures != null && treasures.Length > 0)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.totalTreasures = treasures.Length;
            }
        }

        Debug.Log("Tesoros registrados: " + treasures.Length);
    }
}