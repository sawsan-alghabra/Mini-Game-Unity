using UnityEngine;
using System.Collections;

public class FireExtinguish10s : MonoBehaviour
{
    public ParticleSystem fireParticles;
    public float extinguishTime = 10f;

    private bool extinguishing = false;

    void Awake()
    {
        if (fireParticles == null)
            fireParticles = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        // Debug: siehst du das in der Console, kommt die Collision an
        Debug.Log("FIRE HIT by: " + other.name);

        if (extinguishing) return;
        StartCoroutine(ExtinguishOverTime());
    }

    IEnumerator ExtinguishOverTime()
    {
        extinguishing = true;

        var emission = fireParticles.emission;

        // ROBUST: funktioniert auch bei Curves/Two Constants
        float startRate = emission.rateOverTimeMultiplier;

        float t = 0f;
        while (t < extinguishTime)
        {
            t += Time.deltaTime;
            float k = 1f - (t / extinguishTime);
            emission.rateOverTimeMultiplier = startRate * k;
            yield return null;
        }

        emission.rateOverTimeMultiplier = 0f;
        fireParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }
}


