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
    public class InsulinPen : MonoBehaviour
    {

        public SteamVR_Action_Boolean grabGripAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");
        public SteamVR_Action_Boolean increaseDosis = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("SnapTurnLeft");
        public SteamVR_Action_Boolean decreaseDosis = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("SnapTurnRight");
        public GameObject dosagePart;
        public GameObject needlePart;
        public GameObject usedNeedleObject;
        public Interactable interactable;
        public AudioSource penAudio;
        public Quaternion startRotation;
        public int currentDosage = 1;
        public bool usedNeedle = false;
        public bool instructed = false;
        public int numberOfRotations = 0;

        private bool hovering;

        private Hand lastHoveredHand;
        private Hand currentHand;

        private void Start()
        {
            if (interactable == null)
            {
                interactable = GetComponent<Interactable>();
            }

            startRotation = dosagePart.transform.rotation;

        }

        private void Update()
        {
            if(usedNeedle) 
            {
                if (interactable.attachedToHand && !instructed)
                {
                    ControllerButtonHints.ShowTextHint(currentHand, increaseDosis, "Nadel abschrauben");
                    StartCoroutine(HideHint(currentHand));
                    instructed = true;
                }

                if (increaseDosis.GetStateDown(interactable.attachedToHand.handType))
                {
                    if (numberOfRotations < 4)
                    {
                        numberOfRotations++;
                        needlePart.transform.Rotate(new Vector3(0,0,1));
                        needlePart.GetComponent<AudioSource>().Play();
                    }
                    if (numberOfRotations == 4)
                    {
                        needlePart.GetComponent<AudioSource>().Play();
                        usedNeedleObject.transform.position = needlePart.transform.position;
                        usedNeedleObject.transform.rotation = needlePart.transform.rotation;
                        needlePart.SetActive(false);
                        usedNeedleObject.SetActive(true);
                    }
                }

            }
            else 
            {

                if (interactable.attachedToHand)
                {
                    if (increaseDosis.GetStateDown(interactable.attachedToHand.handType))
                    {
                        if (currentDosage == 10)
                        {
                            //don't rotate
                        }
                        else 
                        {
                            penAudio.Play();
                        }

                        if (currentDosage == 9)
                        {
                            dosagePart.transform.Rotate(new Vector3(0,0,-40));
                            currentDosage++;
                        }

                        if (currentDosage == 8)
                        {
                            dosagePart.transform.Rotate(new Vector3(0,0,-35));
                            currentDosage++;
                        }
                        
                        if (currentDosage == 7)
                        {
                            dosagePart.transform.Rotate(new Vector3(0,0,-40));
                            currentDosage++;
                        }

                        if (currentDosage == 6)
                        {
                            dosagePart.transform.Rotate(new Vector3(0,0,-35));
                            currentDosage++;
                        }

                        if (currentDosage == 5)
                        {
                            dosagePart.transform.Rotate(new Vector3(0,0,-35));
                            currentDosage++;
                        }

                        if (currentDosage == 4)
                        {
                            dosagePart.transform.Rotate(new Vector3(0,0,-35));
                            currentDosage++;
                        }

                        if (currentDosage == 3)
                        {
                            dosagePart.transform.Rotate(new Vector3(0,0,-35));
                            currentDosage++;
                        }

                        if (currentDosage == 2)
                        {
                            dosagePart.transform.Rotate(new Vector3(0,0,-35));
                            currentDosage++;
                        }
                        
                        if (currentDosage == 1)
                        {
                            dosagePart.transform.Rotate(new Vector3(0,0,-35));
                            currentDosage++;
                        }
                    }

                    if (decreaseDosis.GetStateDown(interactable.attachedToHand.handType))
                    {
                        if (currentDosage == 1)
                        {
                            //don't rotate
                        }
                        else 
                        {
                            penAudio.Play();
                        }

                        if (currentDosage == 2)
                        {
                            dosagePart.transform.Rotate(new Vector3(0,0,35));
                            currentDosage--;
                        }

                        if (currentDosage == 3)
                        {
                            dosagePart.transform.Rotate(new Vector3(0,0,35));
                            currentDosage--;
                        }

                        if (currentDosage == 4)
                        {
                            dosagePart.transform.Rotate(new Vector3(0,0,35));
                            currentDosage--;
                        }   

                        if (currentDosage == 5)
                        {
                            dosagePart.transform.Rotate(new Vector3(0,0,35));
                            currentDosage--;
                        }

                        if (currentDosage == 6)
                        {
                            dosagePart.transform.Rotate(new Vector3(0,0,35));
                            currentDosage--;
                        }

                        if (currentDosage == 7)
                        {
                            dosagePart.transform.Rotate(new Vector3(0,0,35));
                            currentDosage--;
                        }

                        if (currentDosage == 8)
                        {
                            dosagePart.transform.Rotate(new Vector3(0,0,40));
                            currentDosage--;
                        }

                        if (currentDosage == 9)
                        {
                            dosagePart.transform.Rotate(new Vector3(0,0,35));
                            currentDosage--;
                        }
                        
                        if (currentDosage == 10)
                        {
                            dosagePart.transform.Rotate(new Vector3(0,0,40));
                            currentDosage--;
                        }            
                    }
                }
            }
        }

        private void OnHandHoverBegin(Hand hand)
        {
            if (gameObject.name == "NovoRapidInsulinPen")
            {
                ControllerButtonHints.ShowTextHint(hand, grabGripAction, "NovoRapid Insulin Pen" );
            }  
            if (gameObject.name == "LantusInsulinPen")
            {
                ControllerButtonHints.ShowTextHint(hand, grabGripAction, "Lantus Insulin Pen" );
            }  
        }

        private void OnHandHoverEnd(Hand hand)
        {
            ControllerButtonHints.HideTextHint(hand, grabGripAction);
        }

        private void OnAttachedToHand(Hand hand)
        {
            currentHand = hand;
            if (currentDosage == 1 && !usedNeedle)
            {
                ControllerButtonHints.ShowTextHint(hand, increaseDosis, "Dosis verändern");
                StartCoroutine(HideHint(hand));
            }
        }

        IEnumerator HideHint(Hand hand)
        {
            yield return new WaitForSeconds(3f);
            ControllerButtonHints.HideTextHint(hand, increaseDosis);
        }

        private void OnDetachedFromHand(Hand hand)
        {
            ControllerButtonHints.HideTextHint(hand, increaseDosis);
        }

    }
}
