//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Interactable that can be used to move in a circular motion
//
//=============================================================================

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

namespace Valve.VR.InteractionSystem
{

	//-------------------------------------------------------------------------
	[RequireComponent(typeof(Interactable))]
	public class SoapInteraction : MonoBehaviour
	{

		public GameObject Soap;
		Collider soapCollider;
        private Interactable interactable;
		public Hand leftHand;
		public Hand rightHand;
		public ParticleSystem leftParticles;
		public ParticleSystem rightParticles;
		public GameObject soapHandsGoal;
		public GameObject tutorial;
		public bool isSoapedAchieved = false;

        private Hand handHoverLocked = null;

        private void Awake()
        {
            interactable = this.GetComponent<Interactable>();
        }

        //-------------------------------------------------
        private void Start()
		{
			soapCollider = GetComponent<Collider>();
			soapCollider.isTrigger = true;		
        }

		//-------------------------------------------------
		void OnDisable()
		{
			if (handHoverLocked)
			{
                handHoverLocked.HideGrabHint();
				handHoverLocked.HoverUnlock(interactable);
				handHoverLocked = null;
			}
		}


		//-------------------------------------------------
		private IEnumerator HapticPulses(Hand hand, float flMagnitude, int nCount)
		{
			if (hand != null)
			{
				int nRangeMax = (int)Util.RemapNumberClamped(flMagnitude, 0.0f, 1.0f, 10.0f, 900.0f);
				nCount = Mathf.Clamp(nCount, 1, 10);

				for (ushort i = 0; i < nCount; ++i)
				{
					ushort duration = (ushort)Random.Range(100, nRangeMax);
					hand.TriggerHapticPulse(duration);
					yield return new WaitForSeconds(.01f);
				}
			}
		}


		//-------------------------------------------------
		private void OnHandHoverBegin(Hand hand)
		{
			if(hand == leftHand && !leftHand.isSoaped)
			{
				leftHand.isSoaped = true;
				leftParticles.startColor = new Color(1f, 0.7f, 0.7f, 1f);
				leftParticles.Play();
				
			}

			if(hand == rightHand && !rightHand.isSoaped)
			{
				rightHand.isSoaped = true;
				rightParticles.startColor = new Color(1f, 0.7f, 0.7f, 1f);
				rightParticles.Play();
			}	

			if (leftHand.isSoaped && rightHand.isSoaped && !isSoapedAchieved){
				soapHandsGoal.GetComponent<Text>().color = Color.green;
				soapHandsGoal.GetComponent<AudioSource>().Play();
				tutorial.GetComponent<TutorialCompletion>().ActionCompleted();
				isSoapedAchieved = true;
			}

			
		}


		//-------------------------------------------------
		private void OnHandHoverEnd(Hand hand)
		{
			Debug.Log("Hand has left");
		}

        private GrabTypes grabbedWithType;
		//-------------------------------------------------
		private void HandHoverUpdate(Hand hand)
        {
			hand.TriggerHapticPulse(1);
		}

	}
}
