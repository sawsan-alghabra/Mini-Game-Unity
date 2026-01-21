using UnityEngine; // <--- DIESE ZEILE HAT GEFEHLT

public class FireExtinguisherPin : MonoBehaviour
{
    public bool pinPulled = false;

    public void OnPinPulled()
    {
        pinPulled = true;
        Debug.Log("Der Stift wurde gezogen!"); // Hilfreich, um es in der Console zu sehen
    }
}