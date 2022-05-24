using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem{
public class InjectVein : MonoBehaviour
{
    
    public GameObject drawSyringe;
    public GameObject pushSyringe;

    private void OnTriggerEnter(Collider syringe)
    {
        Debug.Log("OnTriggerEnter successful.");
        if(syringe.gameObject.name == "SyringeNeedleBody" && drawSyringe.GetComponent<SyringeDraw>().isDrawn == true)
        {
            pushSyringe.SetActive(true);
            Destroy(drawSyringe);
        }
    }


}
}