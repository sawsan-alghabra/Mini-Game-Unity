using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
 
public class FireExtinguisherController : MonoBehaviour
{
    [Header("Pin Setup")]
    public GameObject pinObject;  // Pin-Child im Inspector zuweisen
    [Header("Spray Effects")]
    public ParticleSystem sprayEffect;  // Partikel an Düse
    public GameObject nozzle;           // Düsen-Child (für Raycast)
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable extinguisherGrab;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable pinGrab;
    private bool isPinPulled = false;
    void Start()
    {
        extinguisherGrab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (pinObject) pinGrab = pinObject.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        // Event: Wenn Pin losgelassen wird (gepulled)
        if (pinGrab) pinGrab.selectExited.AddListener(OnPinReleased);
        sprayEffect?.Stop();  // Spray aus
    }
    void OnPinReleased(SelectExitEventArgs args)
    {
        isPinPulled = true;
        Debug.Log("🔥 PIN PULLED! Extinguisher ready.");
        // Pin verstecken
        pinObject.SetActive(false);
    }
    // Trigger für Spray (wird später XR-Action)
    public void StartSpray()
    {
        if (isPinPulled && sprayEffect)
        {
            sprayEffect.Play();
            Debug.Log("Spraying...");
            // Hier kommt später Raycast-Feuer-Logik
        }
    }
    public void StopSpray()
    {
        sprayEffect?.Stop();
    }
    // Getter für andere Scripts (Feuer-Logik)
    public bool IsReady() => isPinPulled;
}