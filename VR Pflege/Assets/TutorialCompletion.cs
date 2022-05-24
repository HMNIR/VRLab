using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Valve.VR.InteractionSystem {
public class TutorialCompletion : MonoBehaviour
{
    public int actionsCompleted = 0;

    public void ActionCompleted(){
        actionsCompleted ++;
        if (actionsCompleted >= 3) {
            this.GetComponent<Text>().color = Color.green;
            this.GetComponent<Text>().text = "Du hast das Tutorial abgeschlossen, Glückwunsch!";
        }
    }
}
}