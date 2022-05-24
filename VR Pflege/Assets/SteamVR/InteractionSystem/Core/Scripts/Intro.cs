//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Drives a linear mapping based on position between 2 positions
//
//=============================================================================

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
    //-------------------------------------------------------------------------
    [RequireComponent(typeof(Interactable))]
    public class Intro : MonoBehaviour
    {

        public GameObject door;
        public GameObject introDoor;
        public GameObject introScreen;
        public Transform player;
        public Vector3 vector = new Vector3(-3.126f,0.1f,-3.529f);
        public Quaternion quaternion = new Quaternion(0,0,0,0);
        public AudioSource intro;
        public bool isGamified;
        public Points pointsSystem;

        private void Start()
        {
            if(isGamified)
            {
                pointsSystem.isGamified = true;
                pointsSystem.gameObject.SetActive(true);
            }
            player.transform.position = vector;
            player.transform.rotation = quaternion;
            StartCoroutine(WaitForFirstAudioToPlay());
        }


        IEnumerator WaitForFirstAudioToPlay ()
        {
            yield return new WaitForSeconds(12.5f);
            StartCoroutine(WaitForSecondAudioToPlay());
        }

        IEnumerator WaitForSecondAudioToPlay ()
        {
            yield return new WaitForSeconds(0.5f);
            door.SetActive(true);
            Destroy(introDoor);
            Destroy(introScreen);
            Destroy(this);
        }
    }
}
