using UnityEngine;

public class FireExtinguisherSpray : MonoBehaviour
{
    [Header("Komponenten")]
    public ParticleSystem foam;           // Das Partikelsystem (Child der Düse)
    public FireExtinguisherPin pin;       // Das Skript des Sicherungsstifts

    [Header("Tracking & Input")]
    public Transform nozzleHand;          // Die Hand (RightControllerAnchor), der die Düse folgt

    void Update()
    {
        // 1. Prüfen: Ist der Pin gezogen?
        bool isPinOut = pin != null && pin.pinPulled;

        // 2. Prüfen: Wird der Trigger am RECHTEN Controller gedrückt?
        // OVRInput ist der stabilste Weg für Meta Quest in Unity 6
        bool triggerPressed = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch);

        // Logik: Nur sprühen, wenn Pin raus UND Trigger gedrückt
        if (isPinOut && triggerPressed)
        {
            if (!foam.isPlaying) foam.Play();
        }
        else
        {
            if (foam.isPlaying) foam.Stop();
        }
    }

    void LateUpdate()
    {
        // Die Düse (und damit der Schaum) folgt der Handposition
        if (nozzleHand != null)
        {
            transform.position = nozzleHand.position;
            transform.rotation = nozzleHand.rotation;
        }
    }
}