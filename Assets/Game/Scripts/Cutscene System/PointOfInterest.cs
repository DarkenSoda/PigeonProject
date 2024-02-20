using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.CutsceneSystem
{
    public class PointOfInterest : MonoBehaviour
    {
        [SerializeField] private Image wayPoint;
        [SerializeField] private List<Transform> followingPoints;
        private CutScene cutScene;
        private bool isCutscenePlayed = false;

        private void Awake() {
            cutScene = GetComponent<CutScene>();
        }
        private void OnTriggerEnter(Collider other) {
            Debug.Log(other.gameObject.tag);
            if (!isCutscenePlayed && other.gameObject.tag == "Player") {
                Debug.Log("Here");
                isCutscenePlayed = true;
                
                wayPoint.gameObject.SetActive(false);
                foreach (Transform point in followingPoints) {
                    PointOfInterest pointOfInterest = point.GetComponent<PointOfInterest>();
                    pointOfInterest.wayPoint.gameObject.SetActive(true);
                }
                cutScene.StartCutScene();
            }
        }

        private void Start() {
            cutScene.CutSceneStartAction += OnCutsceneStart;
            cutScene.CutSceneEndAction += OnCutsceneEnd;
        }

        private void OnCutsceneStart() {
            //to be made.
            Debug.Log("Cutscene started");
        }
        private void OnCutsceneEnd() {
            //to be made.
            Debug.Log("Cutscene Ended");
        }
    }
}
