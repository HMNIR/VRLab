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
	public class DesinfectantLiquid : MonoBehaviour
	{

		[Tooltip("If true, the drive will stay manipulating as long as the button is held down, if false, it will stop if the controller moves out of the collider")]
		public bool hoverLock = false;

		public Hand leftHand;
		public Hand rightHand;
		public ParticleSystem leftParticles;
		public ParticleSystem rightParticles;
		public Reporter reporter;
		public bool eventWritten = false;
		Collider DesinfectantCollider;

		private Hand handHoverLocked = null;

        private Interactable interactable;

        private void Awake()
        {
            interactable = this.GetComponent<Interactable>();
        }

        //-------------------------------------------------
        private void Start()
		{
			DesinfectantCollider = GetComponent<Collider>();
			DesinfectantCollider.isTrigger = true;
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
				int nRangeMax = (int)Util.RemapNumberClamped(flMagnitude, 0.0f, 1.0f, 25.0f, 450.0f);
				nCount = Mathf.Clamp(nCount, 1, 10);

				for (ushort i = 0; i < nCount; ++i)
				{
					ushort duration = (ushort)Random.Range(50, nRangeMax);
					hand.TriggerHapticPulse(duration);
					yield return new WaitForSeconds(.01f);
				}
			}
		}

        private GrabTypes grabbedWithType;
		//-------------------------------------------------
		private void HandHoverUpdate(Hand hand)
		{	
			hand.TriggerHapticPulse(5);

			if (hand == leftHand && !leftHand.isDesinfected)
			{
				leftHand.isDesinfected = true;
				leftParticles.startColor = new Color(1, 1, 1, 1f);
				leftParticles.Play();
			}


			if (hand == rightHand && !rightHand.isDesinfected)
			{
				rightHand.isDesinfected = true;
				rightParticles.startColor = new Color(1, 1, 1, 1f);
				rightParticles.Play();
			}

			if (rightHand.isDesinfected && leftHand.isDesinfected && !eventWritten)
			{
				reporter.WriteEvent("Beide Hände desinfiziert","success",20);
				eventWritten = true;
			}

		}
	}
}
