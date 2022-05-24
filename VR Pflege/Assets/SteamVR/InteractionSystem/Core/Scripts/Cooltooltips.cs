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
    public class Cooltooltips : MonoBehaviour
    {

        public SteamVR_Action_Boolean grabGripAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");
        private bool hovering;

        private Hand lastHoveredHand;

        private void OnHandHoverBegin(Hand hand)
        {
            if(gameObject.tag == "Wattepad")
            {
                ControllerButtonHints.ShowTextHint(hand, grabGripAction, "Wattepad");
            }
            else if(gameObject.tag == "Benutztes Wattepad")
            {
                ControllerButtonHints.ShowTextHint(hand, grabGripAction, "Benutztes Wattepad");
            }
            else if(gameObject.tag == "Desinfektionstüchlein")
            {
                ControllerButtonHints.ShowTextHint(hand, grabGripAction, "Desinfektionstüchlein");
            }
            else if(gameObject.tag == "Benutztes Desinfektionstüchlein")
            {
                ControllerButtonHints.ShowTextHint(hand, grabGripAction, "Benutztes Desinfektionstüchlein");
            }
            else 
            {
                ControllerButtonHints.ShowTextHint(hand, grabGripAction, gameObject.name);
            }
        }

        private void OnHandHoverEnd(Hand hand)
        {
            ControllerButtonHints.HideTextHint(hand, grabGripAction);
        }

        private void OnAttachedToHand(Hand hand)
        {
            ControllerButtonHints.HideTextHint(hand, grabGripAction);
        }

        private void OnDetachedFromHand(Hand hand)
        {
            ControllerButtonHints.HideTextHint(hand, grabGripAction);
        }

    }
}
