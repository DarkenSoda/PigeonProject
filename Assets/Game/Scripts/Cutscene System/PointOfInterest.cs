using System.Collections;
using System.Collections.Generic;
using PigeonProject.Pigeon;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.CutsceneSystem
{
    public class PointOfInterest : MonoBehaviour
    {
        [SerializeField] private Image wayPoint;
        [SerializeField] private GameObject sunRay;
        [SerializeField] private List<Transform> followingPoints;
        [SerializeField] private Flight flightSystem;
        [SerializeField] private bool isPlayable = false;
        private CutScene cutScene;
        private bool isCutscenePlayed = false;

        private void Awake() {
            cutScene = GetComponent<CutScene>();
        }
        private void OnTriggerEnter(Collider other) {
            if (isPlayable && !isCutscenePlayed && other.gameObject.tag == "Player") {
                isCutscenePlayed = true;
                wayPoint.gameObject.SetActive(false);
                sunRay.SetActive(false);
                foreach (Transform point in followingPoints) {
                    PointOfInterest pointOfInterest = point.GetComponent<PointOfInterest>();
                    pointOfInterest.wayPoint.gameObject.SetActive(true);
                    pointOfInterest.sunRay.SetActive(true);
                    pointOfInterest.isPlayable = true;
                }
                cutScene.StartCutScene();
            }
        }

        public void Initialize(){
            wayPoint.gameObject.SetActive(true);
            sunRay.SetActive(true);
            isPlayable = true;
        }

        private void Start() {
            cutScene.CutSceneStartAction += OnCutsceneStart;
            cutScene.CutSceneEndAction.AddListener(OnCutsceneEnd);
        }

        private void OnCutsceneStart() {
            //to be made.
            Debug.Log("Cutscene started");
            flightSystem.CanMove = false;
            // we should make the pigeon go to the last checkpoint too continue the story.
        }
        private void OnCutsceneEnd() {
            //to be made.
            Debug.Log("Cutscene Ended");
            flightSystem.CanMove = true;
            //do whatever you like.
        }


    }


}
