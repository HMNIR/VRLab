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
    public class DisposalBin : MonoBehaviour
    {

        public Reporter reporter;
        public AudioSource disposed;
        public Text medikamente;

        private void Start()
        {

        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.tag == "BenutzteNadel")
            {
                disposed.Play();
                reporter.WriteEvent("Gebrauchte Nadel korrekt entsorgt","success",20);
                medikamente.text = "Medikamente 1/11";
                medikamente.color = Color.green;
                Destroy(collider.gameObject.transform.parent.gameObject);
            }
        }
    }
}
