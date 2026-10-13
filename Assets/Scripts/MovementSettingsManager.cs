using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class MovementSettingsManager : MonoBehaviour
{
    [Header("Movimiento (stick izquierdo)")]
    [SerializeField] ContinuousMoveProvider smoothMove;
    [SerializeField] TeleportationProvider teleportProvider;
    [SerializeField] GameObject[] teleportRays;
    [SerializeField] ControllerInputActionManager leftManager;

    [Header("Giro (stick derecho)")]
    [SerializeField] ControllerInputActionManager rightManager;

    const string MoveKey = "loco_teleport";
    const string TurnKey = "loco_snap";

    void Start()
    {
        SetTeleport(PlayerPrefs.GetInt(MoveKey, 0) == 1);
        SetSnapTurn(PlayerPrefs.GetInt(TurnKey, 0) == 1);
    }

    public void SetTeleport(bool useTeleport)
    {
        smoothMove.enabled = !useTeleport;
        teleportProvider.enabled = useTeleport;
        foreach (var ray in teleportRays) ray.SetActive(useTeleport);

        // Solo la mano izquierda controla el movimiento
        if (leftManager != null) leftManager.smoothMotionEnabled = !useTeleport;

        PlayerPrefs.SetInt(MoveKey, useTeleport ? 1 : 0);
    }

    public void SetSnapTurn(bool useSnap)
    {
        var smooth = FindObjectsByType<ContinuousTurnProvider>(FindObjectsInactive.Include);
        var snap = FindObjectsByType<SnapTurnProvider>(FindObjectsInactive.Include);

        foreach (var s in smooth) s.enabled = !useSnap;
        foreach (var s in snap) s.enabled = useSnap;

        // Solo la mano derecha controla el giro
        if (rightManager != null) rightManager.smoothTurnEnabled = !useSnap;

        PlayerPrefs.SetInt(TurnKey, useSnap ? 1 : 0);
    }

    public void SetMoveDropdown(int index) => SetTeleport(index == 1);
    public void SetTurnDropdown(int index) => SetSnapTurn(index == 1);
}