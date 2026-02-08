using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
 
public class ExtinguisherController : UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable {
    public ParticleSystem foamEffect;  // Particle System zuweisen (Cone-Shape für Strahl)
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable trigger;
    protected override void Awake() {
        base.Awake();
        trigger = GetComponentInChildren<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        trigger.activated.AddListener(StartFoam);
        trigger.deactivated.AddListener(StopFoam);
    }
    void StartFoam(ActivateEventArgs arg) { foamEffect.Play(); }
    void StopFoam(DeactivateEventArgs arg) { foamEffect.Stop(); }

}
