using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

namespace Valve.VR.InteractionSystem
{

    public class Desinfectant : MonoBehaviour
    {
        public GameObject desinfectantLiquid;
        public GameObject desinfectantBody;
        public GameObject desinfectantCap;
        public Hand leftHand;
        public Hand rightHand;

        // Update is called once per frame
        void Update()
        {
            if (!leftHand.isDesinfected || !rightHand.isDesinfected)
            {
                if (desinfectantCap.transform.position.y < desinfectantBody.transform.position.y && !desinfectantLiquid.activeSelf)
                {
                    desinfectantLiquid.SetActive(true);
                }
                if (desinfectantCap.transform.position.y > desinfectantBody.transform.position.y && desinfectantLiquid.activeSelf)
                {
                    desinfectantLiquid.SetActive(false);
                }
            }
            else 
            {
                desinfectantLiquid.SetActive(false);
            }

        }
    }
}