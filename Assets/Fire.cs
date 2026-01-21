using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    [Header("Einstellungen")]
    public ParticleSystem foamParticles;
    
    // Auf public gesetzt, damit du im Inspector den Haken zum Testen setzen kannst
    public bool isPinRemoved = false;

    private AudioSource spraySound;

    void Start()
    {
        // Initialisierung: Partikel stoppen
        if (foamParticles != null)
        {
            foamParticles.Stop();
            // Sucht nach einem Sound am Partikelsystem
            spraySound = foamParticles.GetComponent<AudioSource>();
        }
    }

    // Wird vom Pin-Script aufgerufen oder durch das Event "Select Entered"
    public void RemovePin()
    {
        isPinRemoved = true;
        Debug.Log("Pin entfernt! Feuerlöscher einsatzbereit.");
    }

    // Verknüpfe dies mit "Activated" am XR Grab Interactable
    public void TriggerPressed()
    {
        if (isPinRemoved && foamParticles != null)
        {
            if (!foamParticles.isPlaying)
            {
                foamParticles.Play();
                if (spraySound != null) spraySound.Play();
                Debug.Log("Löschschaum tritt aus!");
            }
        }
        else if (!isPinRemoved)
        {
            Debug.Log("Sicherung gesperrt: Du musst erst den Pin ziehen!");
        }
    }

    // Verknüpfe dies mit "Deactivated" am XR Grab Interactable
    public void TriggerReleased()
    {
        if (foamParticles != null)
        {
            foamParticles.Stop();
            if (spraySound != null) spraySound.Stop();
            Debug.Log("Löschvorgang gestoppt.");
        }
    }
}