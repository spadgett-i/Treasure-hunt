using UnityEngine;
using TMPro;

public class TreasureCounterUI : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI counterText;

    private void Start()
    {
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        if (GameManager.Instance == null)
            return;

        int found = GameManager.Instance.TreasuresFound;
        int total = GameManager.Instance.totalTreasures;

        counterText.text = "Tesoros: " + found + " / " + total;
    }

    public void RefreshCounter()
    {
        UpdateCounter();
    }
}