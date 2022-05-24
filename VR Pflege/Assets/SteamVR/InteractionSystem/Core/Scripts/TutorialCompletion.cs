using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Valve.VR.InteractionSystem {
public class TutorialCompletion : MonoBehaviour
{
    public int actionsCompleted = 0;
    public bool audioHasPlayed = false;
    public GameObject Simulationsstartbutton;

    public void ActionCompleted(){
        actionsCompleted ++;
        CheckActions();

    }

    public void ActionRemoved(){
        actionsCompleted --;
        CheckActions();
    }

    public void CheckActions(){

        if (actionsCompleted >= 3) {
            this.GetComponent<Text>().color = Color.green;
            this.GetComponent<Text>().text = "Du hast das Tutorial abgeschlossen, Glückwunsch! \n Per Knopfdruck geht es in deine erste Simulation!";
            if (!audioHasPlayed) {
                audioHasPlayed = true;
                this.GetComponent<AudioSource>().Play();
                Simulationsstartbutton.SetActive(true);
            }
        } 
        else {
            this.GetComponent<Text>().color = Color.white;
            this.GetComponent<Text>().text = "Willkommen im Tutorial! \n Lerne die Steuerung kennen \n indem du die beiden Aufgaben im Raum löst.";
        }



    }

}
}