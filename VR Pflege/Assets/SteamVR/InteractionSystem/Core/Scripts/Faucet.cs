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
	public class Faucet : MonoBehaviour
	{

		[Tooltip("If true, the drive will stay manipulating as long as the button is held down, if false, it will stop if the controller moves out of the collider")]
		public bool hoverLock = false;

		public Hand leftHand;
		public Hand rightHand;
		public ParticleSystem leftParticles;
		public ParticleSystem rightParticles;
		public GameObject washHandsGoal;
		public GameObject tutorial;

		public GameObject water;
		Collider faucetCollider;

		private Hand handHoverLocked = null;

        private Interactable interactable;

		private bool isWashed = false;

        private void Awake()
        {
            interactable = this.GetComponent<Interactable>();
        }

        //-------------------------------------------------
        private void Start()
		{
			faucetCollider = GetComponent<Collider>();
			faucetCollider.isTrigger = true;
			water.SetActive(false);
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
				int nRangeMax = (int)Util.RemapNumberClamped(flMagnitude, 0.0f, 1.0f, 50.0f, 900.0f);
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
            water.SetActive(true);
			Debug.Log("Water is running");
		}


		//-------------------------------------------------
		private void OnHandHoverEnd(Hand hand)
		{
			water.SetActive(false);
			isWashed = false;
			Debug.Log("Water is no longer running");
		}

        private GrabTypes grabbedWithType;
		//-------------------------------------------------
		private void HandHoverUpdate(Hand hand)
		{	
			hand.TriggerHapticPulse(100);

			Debug.Log("Needs more Soap.");
			if(leftHand.isSoaped==true && rightHand.isSoaped==true)
			{
				/*while (leftHand.GetComponent<Collider>() == rightHand.getComponent<Collider>())
				while (collision.LeftHand && collision.rightHand)
				{
					Debug.Log("Now Washing...");
				} */
				StartCoroutine(WashingHands(10));
				if(isWashed && !leftHand.isClean && !rightHand.isClean)
				{
					leftHand.isClean = true;
					leftHand.isSoaped = false;
					leftHand.isDesinfected = false;
					rightHand.isClean = true;
					rightHand.isSoaped = false;
					rightHand.isDesinfected = false;
					leftParticles.startColor = new Color(1, 1, 1, 1f);
					leftParticles.Play();
					rightParticles.startColor = new Color(1, 1, 1, 1f);
					rightParticles.Play();
					washHandsGoal.GetComponent<Text>().color = Color.green;
					washHandsGoal.GetComponent<AudioSource>().Play();
					tutorial.GetComponent<TutorialCompletion>().ActionCompleted();
				}

			}
		}

		IEnumerator WashingHands (float step)
		{
			yield return new WaitForSeconds(step);
			isWashed = true;
		}

	}
}
