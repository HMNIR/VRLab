using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Valve.VR.InteractionSystem
{

	//-------------------------------------------------------------------------
	[RequireComponent(typeof(Interactable))]
	public class StartSimulation : MonoBehaviour
	{
		public Hand leftHand;
		public Hand rightHand;
		Collider ButtonCollider;
		public GameObject oldPlayer;

		private Hand handHoverLocked = null;

        private Interactable interactable;

        private void Awake()
        {
            interactable = this.GetComponent<Interactable>();
        }

        //-------------------------------------------------
        private void Start()
		{
			ButtonCollider = GetComponent<Collider>();
			ButtonCollider.isTrigger = true;
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
		private void OnHandHoverBegin(Hand hand)
		{
			leftHand.isDesinfected = false;
			leftHand.isSoaped = false;
			leftHand.isClean = false;
			rightHand.isDesinfected = false;
			rightHand.isSoaped = false;
			rightHand.isClean = false;
            SceneManager.LoadScene(1);
			Destroy(oldPlayer);
		}

    }
}