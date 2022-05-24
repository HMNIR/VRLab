using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Valve.VR.InteractionSystem
{

	//-------------------------------------------------------------------------
	[RequireComponent(typeof(Interactable))]
	public class Points : MonoBehaviour
	{
		public int points;
		public Text pointsText;
		public Text isDoneText;
		public Text deinRang;
		public Text rangName;
		public bool isGamified;
		public GameObject BadgeWall;
		public AudioSource adding;
		public AudioSource subtracting;
		public GameObject bronzeBadge;
		public GameObject silverBadge;
		public GameObject goldBadge;
        //-------------------------------------------------
        private void Start()
		{
			if (isGamified)
			{
				BadgeWall.SetActive(false);
				points = 0;
			}
			else
			{
				Destroy(this);
			}
		}

		public void Update()
		{
			pointsText.text = points + " / 180";
		}

		public void ChangePoints(int changedPoints, bool lastEvent)
		{
			points += changedPoints;
			if (changedPoints > 0)
			{
				adding.Play();
			}
			else 
			{
				subtracting.Play();
			}

			if (points < 0)
			{
				points = 0;
			}

			if (lastEvent)
			{
				isDoneText.gameObject.SetActive(true);
				deinRang.gameObject.SetActive(true);
				rangName.gameObject.SetActive(true);
				if (points > 150)
				{
					rangName.text = "Gold";
					goldBadge.SetActive(true);
				}
				else if (points > 100)
				{
					rangName.text = "Silber";
					silverBadge.SetActive(true);
				}
				else 
				{
					rangName.text = "Bronze";
					bronzeBadge.SetActive(true);
				}

			}
		}
    }
}