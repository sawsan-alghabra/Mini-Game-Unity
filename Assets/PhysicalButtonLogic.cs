using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class FireSimulationLogic : MonoBehaviour
{
    [Header("VR Objekte")]
    // Das Feuer-Partikelsystem (GameObject)
    public GameObject fireSystem; 
    
    // Die Info Tafel aus dem Canvas (GameObject oder RectTransform)
    // Wenn GameObject nicht geht, probiere hier: public RectTransform infoTafel;
    public GameObject infoTafel;  

    [Header("Shelly Einstellungen")]
    public string shellyIP = "192.168.1.50"; 

    [Header("Button Physik")]
    [Tooltip("Wie tief muss der Knopf in Y-Richtung gedrückt werden?")]
    public float druckSchwelle = -0.05f; 
    
    private bool wurdeGedrueckt = false;
    private Rigidbody rb;
    private Vector3 startPosition;

    void Start()
    {
        // Holt sich den Rigidbody vom Button
        rb = GetComponent<Rigidbody>();
        
        // Speichert die Ausgangsposition
        startPosition = transform.localPosition;
        
        // Initialisierung: Alles aus am Anfang
        if(fireSystem != null) fireSystem.SetActive(false);
        if(infoTafel != null) infoTafel.SetActive(false);
    }

    void Update()
    {
        // Wenn der Knopf bereits eingerastet ist, mache nichts mehr
        if (wurdeGedrueckt) return;

        // Berechne die aktuelle Verschiebung auf der Y-Achse
        float aktuelleTiefe = transform.localPosition.y - startPosition.y;

        // Wenn die Schwelle unterschritten wird (Knopf tief genug)
        if (aktuelleTiefe <= druckSchwelle)
        {
            AktiviereSimulation();
        }
    }

    void AktiviereSimulation()
    {
        wurdeGedrueckt = true;

        // 1. PHYSIK: Button "einfrieren" (Einrasten)
        rb.isKinematic = true; 
        transform.localPosition = new Vector3(startPosition.x, startPosition.y + druckSchwelle, startPosition.z);

        // 2. VISUELL: Feuer und Info Tafel einschalten
        if(fireSystem != null) fireSystem.SetActive(true);
        if(infoTafel != null) infoTafel.SetActive(true);

        // 3. HARDWARE: Shelly Heizlüfter einschalten
        StartCoroutine(SendShellyRequest("on"));

        Debug.Log("Simulation aktiv: Feuer, Tafel und Shelly eingeschaltet.");
    }

    // Hilfsfunktion für die Kommunikation mit dem Shelly Plug
    IEnumerator SendShellyRequest(string state)
    {
        // Standard Shelly API URL
        string url = $"http://{shellyIP}/relay/0?turn={state}"; 
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning("Shelly Fehler: " + www.error);
            }
        }
    }
}