using UnityEngine;
// Wir brauchen hier kein XR-Toolkit-Using mehr, da wir OVRInput nutzen

public class Duese : MonoBehaviour 
{
    public ParticleSystem foam;
    public FireExtinguisherPin pinScript;
    public Transform hand; 

    void Update()
    {
        // 1. Prüfen, ob der Pin gezogen wurde
        bool isPinOut = pinScript != null && pinScript.pinPulled;

        // 2. Prüfen, ob der Trigger am RECHTEN Controller gedrückt wird (Meta Standard)
        // OVRInput funktioniert stabil in Unity 6 für Meta-Hardware
        bool triggerPressed = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch);

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
        // Die Düse folgt der Hand
        if (hand != null)
        {
            transform.position = hand.position;
            transform.rotation = hand.rotation;
        }
    }
}