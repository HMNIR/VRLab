using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Valve.VR.InteractionSystem
{

	//-------------------------------------------------------------------------
	[RequireComponent(typeof(Interactable))]
	public class StartButton : MonoBehaviour
	{

		[Tooltip("If true, the drive will stay manipulating as long as the button is held down, if false, it will stop if the controller moves out of the collider")]
		public bool hoverLock = false;

		public Hand leftHand;
		public Hand rightHand;
		Collider ButtonCollider;

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
            SceneManager.LoadScene(1);
		}

    }
}