using UnityEngine;
 
public class FireExtinguisher : MonoBehaviour
{
    public ParticleSystem foamParticles;
    private bool isPinRemoved = false;
 
    // Wird vom Pin-Script aufgerufen
    public void RemovePin()
    {
        isPinRemoved = true;
        Debug.Log("Pin entfernt! Feuerlöscher einsatzbereit.");
    }
 
    // Diese Methode verknüpfst du mit "Activated" am Hauptgriff
    public void TriggerPressed()
    {
        if (isPinRemoved && foamParticles != null)
        {
            foamParticles.Play();
        }
        else if (!isPinRemoved)
        {
            Debug.Log("Du musst erst den Pin ziehen!");
        }
    }
 
    // Diese Methode verknüpfst du mit "Deactivated"
    public void TriggerReleased()
    {
        if (foamParticles != null)
        {
            foamParticles.Stop();
        }
    }
}