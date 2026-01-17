
using UnityEngine;

using UnityEngine.XR.Interaction.Toolkit;
 
public class PinPull : MonoBehaviour

{

    public bool pinRemoved = false;

    private FixedJoint joint;
 
    void Start()

    {

        joint = GetComponent<FixedJoint>();

    }
 
    // WICHTIG: Richtige Signatur für XR Events

    public void OnGrab(SelectEnterEventArgs args)

    {

        if (joint != null)

        {

            Destroy(joint);

            pinRemoved = true;

        }

    }

}

 