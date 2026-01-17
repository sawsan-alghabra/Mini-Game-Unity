using UnityEngine;
 
public class PinPuller : MonoBehaviour
{
    public FireExtinguisher mainScript;
 
    // Wird aufgerufen, wenn der Pin gegriffen wird
    public void OnPinGrabbed()
    {
        mainScript.RemovePin();
        // Deaktiviert den Pin, damit er "weg" ist
        gameObject.SetActive(false); 
    }
}