//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Drives a linear mapping based on position between 2 positions
//
//=============================================================================

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
    //-------------------------------------------------------------------------
    [RequireComponent(typeof(Interactable))]
    public class FingerDraw : MonoBehaviour
    {
        public GameObject correspondingBlood;
        public GameObject bloodStripeBlood;
        public ParticleSystem desinfectedFinger;
        public GlucoseTest glucoseTest;
        public GameObject audioSource;
        public Reporter reporter;
        public bool isDesinfected = false;
        public bool isDrawn = false;
        public bool isWiped = false;
        public bool isMeasured = false;

        private bool hovering;

        private Hand lastHoveredHand;

        private void Start()
        {
            correspondingBlood.SetActive(false);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.tag == "Desinfektionstüchlein" && !isDesinfected &&!isDrawn)
            {
                isDesinfected = true;
                desinfectedFinger.Play();
                collider.gameObject.tag = "Benutztes Desinfektionstüchlein";
                collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                collider.gameObject.transform.parent.gameObject.tag = "Benutztes Desinfektionstüchlein";
                reporter.WriteEvent("Finger vor dem Anstechen desinfiziert","success",20);
            }

            if (collider.gameObject.tag == "Stechlanzette" && !isDrawn){
                audioSource.GetComponent<AudioSource>().Play();
                reporter.WriteEvent("Blut abgenommen","success",20);
                if(!isDesinfected)
                {
                    
                    reporter.WriteEvent("Finger vor dem Anstechen nicht desinfiziert","failure",-20);
                }
                correspondingBlood.SetActive(true);
                isDrawn = true;
            }

            if (collider.gameObject.tag == "Wattepad" && isDrawn && !isWiped)
            {
                if(isDesinfected)
                {
                    reporter.WriteEvent("Finger nach dem Anstechen abgewischt","success",20);
                    isWiped = true;
                }                
                collider.gameObject.tag = "Benutztes Wattepad";
                collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                collider.gameObject.transform.parent.gameObject.tag = "Benutztes Wattepad";
            }

            if (collider.gameObject.tag == "Blutmessgerät" && isDrawn && !isMeasured)
            {
                reporter.WriteEvent("Blutzuckermessung vorgenommen","success",20);
                bloodStripeBlood.SetActive(true);
                glucoseTest.hasBloodContact = true;
                if(!isWiped)
                {
                    reporter.WriteEvent("Finger nach dem Anstechen nicht abgewischt","warning",0);
                }
                isMeasured = true;
            }
        }
    }
}
