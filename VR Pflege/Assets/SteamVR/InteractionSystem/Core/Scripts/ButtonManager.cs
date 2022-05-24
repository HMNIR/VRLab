using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Valve.VR.InteractionSystem
{

	//-------------------------------------------------------------------------
	[RequireComponent(typeof(Interactable))]
	public class ButtonManager : MonoBehaviour
	{
		public Hand leftHand;
		public Hand rightHand;
		Collider ButtonCollider;
		public GameObject myActiveButton;
		public GameObject activeButton1;
		public GameObject inactiveButton1;
		public GameObject activeButton2;
		public GameObject inactiveButton2;

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
			myActiveButton.SetActive(true);
			activeButton1.SetActive(false);
			activeButton2.SetActive(false);
			inactiveButton1.SetActive(true);
			inactiveButton2.SetActive(true);
			this.gameObject.SetActive(false);
		}

    }
}