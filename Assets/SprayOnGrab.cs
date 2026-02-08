using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SprayOnGrab : MonoBehaviour
{
    [SerializeField] private ParticleSystem spray;
    [SerializeField] private AudioSource sprayAudio;

    private XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();

        // Spray finden falls nicht zugewiesen
        if (spray == null)
            spray = GetComponentInChildren<ParticleSystem>(true);

        // AudioSource finden falls nicht zugewiesen
        if (sprayAudio == null)
            sprayAudio = GetComponentInChildren<AudioSource>(true);

        // Spray am Anfang stoppen
        if (spray != null)
        {
            spray.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

            // Partikel-Collision für Feuer aktivieren
            var collision = spray.collision;
            collision.enabled = true;
            collision.sendCollisionMessages = true;
            collision.type = ParticleSystemCollisionType.World;
        }

        // AudioSource vorbereiten
        if (sprayAudio != null)
        {
            sprayAudio.playOnAwake = false;
            sprayAudio.loop = true;      // wichtig für Dauer-Spray
            sprayAudio.spatialBlend = 1f; // 3D sound für VR
        }
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
        if (spray != null)
            spray.Play();

        if (sprayAudio != null && !sprayAudio.isPlaying)
            sprayAudio.Play();
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        if (spray != null)
            spray.Stop(true, ParticleSystemStopBehavior.StopEmitting);

        if (sprayAudio != null && sprayAudio.isPlaying)
            sprayAudio.Stop();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Fire"))
        {
            Debug.Log("Sprühe auf Feuer: " + other.name);
        }
    }
}
