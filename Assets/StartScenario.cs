using UnityEngine;

public class StartScenario : MonoBehaviour
{
    public GameObject infoPanel;   // Canvas/Infotafel
    public GameObject fireObject;  // Feuer (am Anfang aus)

    public void StartTraining()
    {
        if (infoPanel) infoPanel.SetActive(true);
        if (fireObject) fireObject.SetActive(true);
        Debug.Log("Training started");
    }
}
