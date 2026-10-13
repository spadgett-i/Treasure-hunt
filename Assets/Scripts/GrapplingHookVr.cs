using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrapplingHookVR : MonoBehaviour
{
    [Header("Referencias XR")]
    public Transform firePoint;
    public Transform playerXR;
    public LineRenderer lineRenderer;

    [Header("Configuración del Gancho")]
    public float maxDistance = 25f;
    public float pullSpeed = 15f;

    private XRGrabInteractable grabInteractable;
    private Vector3 grapplePoint;
    private bool isGrappling = false;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grabInteractable.activated.AddListener(StartGrapple);
        grabInteractable.deactivated.AddListener(StopGrapple);
        // Suscribirse al renderizado de la cámara para eliminar el retraso de 1 frame
        RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
    }

    private void OnDisable()
    {
        grabInteractable.activated.RemoveListener(StartGrapple);
        grabInteractable.deactivated.RemoveListener(StopGrapple);
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
    }

    private void StartGrapple(ActivateEventArgs args)
    {
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, maxDistance))
        {
            if (hit.transform.root == playerXR.root || hit.transform == transform)
                return;

            grapplePoint = hit.point;
            isGrappling = true;

            if (lineRenderer != null)
            {
                lineRenderer.enabled = true;
                UpdateLinePositions();
            }
        }
    }

    private void StopGrapple(DeactivateEventArgs args)
    {
        StopGrappleManual();
    }

    private void FixedUpdate()
    {
        if (isGrappling)
        {
            // Mover al jugador en el ciclo de físicas (FixedUpdate)
            Vector3 targetPosition = grapplePoint - (firePoint.forward * 1.0f);
            playerXR.position = Vector3.MoveTowards(playerXR.position, targetPosition, pullSpeed * Time.fixedDeltaTime);

            if (Vector3.Distance(playerXR.position, grapplePoint) < 2.0f)
            {
                StopGrappleManual();
            }
        }
    }

    // Se ejecuta justo antes de que la cámara dibuje la imagen en la pantalla
    private void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        if (isGrappling)
        {
            UpdateLinePositions();
        }
    }

    private void UpdateLinePositions()
    {
        if (lineRenderer != null && lineRenderer.enabled)
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, grapplePoint);
        }
    }

    private void StopGrappleManual()
    {
        isGrappling = false;
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }
}