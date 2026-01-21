using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SprayOnGrab : MonoBehaviour
{
    [SerializeField] private ParticleSystem spray;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        if (spray == null)
            spray = GetComponentInChildren<ParticleSystem>(true);

        // Sicherheit: beim Start aus
        if (spray != null) spray.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    void OnEnable()
    {
        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        grab.selectEntered.RemoveListener(OnGrab);
        grab.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (spray != null) spray.Play();
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        if (spray != null) spray.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }
}
