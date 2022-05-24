//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

namespace Valve.VR.InteractionSystem.Sample
{
    public class DispenserButton : MonoBehaviour
    {
        public GameObject Soap;

        public void OnButtonDown(Hand fromHand)
        {
            fromHand.TriggerHapticPulse(1000);
            Soap.SetActive(true);
        }

        public void OnButtonUp(Hand fromHand)
        {
            Soap.SetActive(false);
        }

        public void OnButtonIsPressed(Hand fromHand)
        {
            Debug.Log("The button is being pressed.");
        }

        private void ColorSelf(Color newColor)
        {
            Renderer[] renderers = this.GetComponentsInChildren<Renderer>();
            for (int rendererIndex = 0; rendererIndex < renderers.Length; rendererIndex++)
            {
                renderers[rendererIndex].material.color = newColor;
            }
        }
    }
}