using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Valve.VR.InteractionSystem
{

	//-------------------------------------------------------------------------
	[RequireComponent(typeof(Interactable))]
	public class Reporter : MonoBehaviour
	{
		public Hand leftHand;
		public Hand rightHand;
		public GameObject reporter;
		public Color success;
		public Color warning;
		public Color failure;
		public int childSelector = 0;
		public int actionsWithoutDesinfectant = 0;
		public GameObject currentChild;
		public Text currentText;
		public string finalDesinfectionReport;
		public Points pointsSystem;
		public bool lastEvent = false;
        //-------------------------------------------------
        private void Start()
		{

		}

		public void WriteEvent(string eventContent, string eventCategory, int points)
		{
			childSelector++;
			currentChild = this.gameObject.transform.GetChild(childSelector).gameObject;
			currentChild.SetActive(true);
			currentText = currentChild.GetComponent<Text>();
			if (eventCategory == "success")
			{
				currentText.color = success;
			}
			if (eventCategory == "warning")
			{
				currentText.color = warning;
			}
			if (eventCategory == "failure")
			{
				currentText.color = failure;
			}
			currentText.text = eventContent;

			if (!leftHand.isDesinfected || !rightHand.isDesinfected)
			{
				actionsWithoutDesinfectant++;
			}

			if (eventContent == "Gebrauchte Nadel korrekt entsorgt" && actionsWithoutDesinfectant > 0)
			{
				lastEvent = true;
				pointsSystem.ChangePoints(points, lastEvent);
				finalDesinfectionReport = actionsWithoutDesinfectant + " Anzahl Aktionen mit nicht desinfizierten Händen";
				WriteEvent(finalDesinfectionReport,"failure",(actionsWithoutDesinfectant * 10 * (-1)));
			}
			else if (eventContent == "Gebrauchte Nadel korrekt entsorgt")
			{
				lastEvent = true;
				pointsSystem.ChangePoints(points, lastEvent);
			}
			else if (pointsSystem.isGamified)
			{
				pointsSystem.ChangePoints(points, lastEvent);
			}

		}

    }
}