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
    public class GlucoseTest : MonoBehaviour
    {

        private bool hovering;
        public bool isOn = false;
        public bool isBeingHold = false;
        public GameObject offScreen;
        public GameObject introScreen;
        public bool hasBloodContact = false;
        public int measureRounds = 0;
        public GameObject measureScreen1;
        public GameObject measureScreen2;
        public GameObject measureScreen3;
        public bool hasCompleted = false;
        public GameObject resultScreen;
        public AudioSource startUpSound;
        public AudioSource measureBeepSound;
        public AudioSource resultBeepSound;
        public GameObject reporter;

        private Hand lastHoveredHand;

        public void Start()
        {

        }

        public void Update()
        {
            if(hasBloodContact && !hasCompleted)
            {
                hasCompleted = true;
                StartCoroutine(MeasuringProcess1());
            }
        }

        private void OnAttachedToHand(Hand hand)
        {
            isBeingHold = true;
            if(!isOn)
            {
                startUpSound.Play();
                isOn = true;
                offScreen.SetActive(false);
                introScreen.SetActive(true);
                
            }
        }

        private void OnDetachedFromHand(Hand hand)
        {
            isBeingHold = false;
            if(hasBloodContact)
            {
                //Do not shut down once blood is drawn and on the strip
            }
            else
            {
                //Shut down after 10s if strip is unused and device is layed down again
                StartCoroutine(AutomaticShutdown());
            }
        }

        IEnumerator AutomaticShutdown ()
        {
            yield return new WaitForSeconds(10f);
            //If not picked up again within 10 seconds, shut down device
            if (!isBeingHold)
            {
                isOn = false;
                introScreen.SetActive(false);
                offScreen.SetActive(true);
            }
        }


        IEnumerator MeasuringProcess1 ()
		{
            introScreen.SetActive(false);
            measureScreen1.SetActive(true);
            measureBeepSound.Play();
			yield return new WaitForSeconds(1.5f);
            StartCoroutine(MeasuringProcess2());
		}

        IEnumerator MeasuringProcess2 ()
        {
            measureScreen1.SetActive(false);
            measureScreen2.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            measureRounds++;
            if(measureRounds < 3)
            {
                StartCoroutine(MeasuringProcess3());
            }
            else
            {
                GotResult();
            }
        }

        IEnumerator MeasuringProcess3 ()
        {
            measureScreen2.SetActive(false);
            measureScreen3.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(MeasuringProcess4());
        }

        IEnumerator MeasuringProcess4 ()
        {
            measureScreen3.SetActive(false);
            measureScreen1.SetActive(true);
            measureBeepSound.Play();
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(MeasuringProcess2());
        }

        public void GotResult ()
        {
            resultBeepSound.Play();
            measureScreen2.SetActive(false);
            resultScreen.SetActive(true);
            //Log Success
        }

    }
}
