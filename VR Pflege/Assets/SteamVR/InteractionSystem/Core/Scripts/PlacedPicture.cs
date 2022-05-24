using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Valve.VR.InteractionSystem {

public class PlacedPicture : MonoBehaviour
{

    public GameObject movePictureGoal;
    public GameObject tutorial;
    public bool audioHasPlayed = false;

    public void OnTriggerEnter(Collider picture){

        if(picture.gameObject.name == "PictureBody")
        {
            if (!audioHasPlayed){
                audioHasPlayed = true;
                movePictureGoal.GetComponent<AudioSource>().Play();
            }
            movePictureGoal.GetComponent<Text>().color = Color.green;
		    tutorial.GetComponent<TutorialCompletion>().ActionCompleted();
        }

    }

    public void OnTriggerExit(Collider picture){

        if(picture.gameObject.name == "PictureBody")
        {
         movePictureGoal.GetComponent<Text>().color = Color.white;
		 tutorial.GetComponent<TutorialCompletion>().ActionRemoved();
        }

    }




}
}
