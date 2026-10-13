using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Treasure : MonoBehaviour
{
    private bool collected = false;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    private void OnEnable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
        }
    }

    private void OnDisable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        Collect();
    }

    public void Collect()
    {
        if (collected)
            return;

        collected = true;

        Debug.Log("Tesoro encontrado: " + gameObject.name);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.TreasureCollected();
        }

        gameObject.SetActive(false);
    }
}