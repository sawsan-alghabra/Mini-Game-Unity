using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
 
public class NozzleController : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;
    private FireExtinguisherController parentController;
    void Start()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        parentController = GetComponentInParent<FireExtinguisherController>();
        grab.selectEntered.AddListener(OnGrabbed);
    }
    void OnGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log("Düse gegriffen!");
        // Hier Trigger-Input für Spray einbauen
    }
}