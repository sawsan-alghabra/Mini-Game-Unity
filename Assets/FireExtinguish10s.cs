using UnityEngine;
using System.Collections;

public class FireExtinguish10s : MonoBehaviour
{
    public ParticleSystem fireParticles;
    [Header("Einstellungen")]
    public float extinguishTime = 5f; // Von 10 auf 5 Sekunden verkürzt

    private bool extinguishing = false;

    void Awake()
    {
        // Falls im Inspector nicht zugewiesen, such das Partikelsystem am Objekt selbst
        if (fireParticles == null)
            fireParticles = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        // Prüft, ob das Objekt, das uns trifft, Partikel sendet (dein Spray)
        Debug.Log("FEUER GETROFFEN VON: " + other.name);

        if (extinguishing) return;
        
        // Startet das langsame Ausgehen
        StartCoroutine(ExtinguishOverTime());
    }

    IEnumerator ExtinguishOverTime()
    {
        extinguishing = true;
        var emission = fireParticles.emission;

        // Speichert die aktuelle Stärke der Flammen
        float startRate = emission.rateOverTimeMultiplier;

        float t = 0f;
        while (t < extinguishTime)
        {
            t += Time.deltaTime;
            // Berechnet, wie viel Prozent der Zeit um sind
            float k = 1f - (t / extinguishTime);
            
            // Macht die Flammen schrittweise weniger
            emission.rateOverTimeMultiplier = startRate * k;
            yield return null;
        }

        // Am Ende alles komplett aus
        emission.rateOverTimeMultiplier = 0f;
        fireParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        
        // Optional: Deaktiviert das Objekt nach dem Löschen komplett
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false); 
    }
}