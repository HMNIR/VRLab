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
    public class InsulinInjection : MonoBehaviour
    {
        public ParticleSystem desinfectedBelly;
        public InsulinPen novoRapid;
        public InsulinPen lantus;
        public GameObject audioSource;
        public InsulinInjection otherInjectionArea;
        public Reporter reporter;
        public bool isDesinfected = false;
        public bool isInjected = false;

        private bool hovering;

        private Hand lastHoveredHand;

        private void Start()
        {

        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.tag == "Desinfektionstüchlein" && !isDesinfected && !isInjected)
            {
                reporter.WriteEvent("Bauch vor der Injektion desinfiziert","success",20);
                desinfectedBelly.Play();
                collider.gameObject.tag = "Benutztes Desinfektionstüchlein";
                collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                collider.gameObject.transform.parent.gameObject.tag = "Benutztes Desinfektionstüchlein";
                isDesinfected = true;
                otherInjectionArea.isDesinfected = true;
            }

            if (collider.gameObject.tag == "NovoRapid" && !isInjected)
            {
                reporter.WriteEvent("Korrekte Art von Insulin verabreicht","success",20);
                novoRapid.usedNeedle = true;
                novoRapid.GetComponent<AudioSource>().Play();
                audioSource.GetComponent<AudioSource>().Play();
                if (!isDesinfected)
                {
                    reporter.WriteEvent("Bauch nicht vor der Injektion desinfiziert","failure",-20);
                }
                if (novoRapid.currentDosage == 2)
                {
                    reporter.WriteEvent("Korrekte Menge an Insulin verabreicht","success",20);
                }
                if (novoRapid.currentDosage < 2)
                {
                    reporter.WriteEvent("Zu wenig Insulin verabreicht","failure",-20);
                }
                if (novoRapid.currentDosage > 2)
                {
                    reporter.WriteEvent("Zu viel Insulin verabreicht","failure",-20);
                }
                otherInjectionArea.isInjected = true;
                isInjected = true;

            }

            if (collider.gameObject.tag == "Lantus" && !isInjected)
            {
                reporter.WriteEvent("Falsche Art von Insulin verabreicht","warning",0);
                lantus.usedNeedle = true;
                lantus.GetComponent<AudioSource>().Play();
                audioSource.GetComponent<AudioSource>().Play();
                if (!isDesinfected)
                {
                    reporter.WriteEvent("Bauch nicht vor der Injektion desinfiziert","failure",-20);
                }
                if (isDesinfected && lantus.currentDosage == 2)
                {
                    reporter.WriteEvent("Korrekte Menge an Insulin verabreicht","success",20);
                }
                if (lantus.currentDosage < 2)
                {
                    reporter.WriteEvent("Zu wenig Insulin verabreicht","failure",-20);
                }
                if (lantus.currentDosage > 2)
                {
                    reporter.WriteEvent("Zu viel Insulin verabreicht","failure",-20);
                }
                otherInjectionArea.isInjected = true;
                isInjected = true;
            }
        }
    }
}
