using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.CutsceneSystem {
    public class CutScene : MonoBehaviour {
        [SerializeField] private CutSceneSO cutSceneSO;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Transform imageContainer;
        private List<Image> imagePrefabs = new();
        private int currentDisplayedImage = 0;
        public Action CutSceneStartAction;
        public Action CutSceneEndAction;
        private bool isStarted = false;
        private bool isFinished = false;
        public void StartCutScene() {
            if (!isFinished) {
                CutSceneStartAction?.Invoke();
                for (int i = 0; i < cutSceneSO.imageList.Count; i++) {
                    Image image = Instantiate(cutSceneSO.imageList[i], imageContainer);
                    imagePrefabs.Add(image);
                    image.gameObject.SetActive(false);
                }
                currentDisplayedImage = 0;
                imagePrefabs[currentDisplayedImage].gameObject.SetActive(true);
                audioSource.clip = cutSceneSO.audioList[currentDisplayedImage];
                audioSource.Play();
                isStarted = true;
            }
        }
        private void Update() {
            if (isStarted) {
                if (!audioSource.isPlaying) {
                    imagePrefabs[currentDisplayedImage].gameObject.SetActive(false);
                    currentDisplayedImage++;
                    if (currentDisplayedImage >= cutSceneSO.imageList.Count) {
                        CutSceneEndAction?.Invoke();
                        isStarted = false;
                        isFinished = true;
                    } else {

                        audioSource.clip = cutSceneSO.audioList[currentDisplayedImage];
                        imagePrefabs[currentDisplayedImage].gameObject.SetActive(true);
                    }
                    
                }
            }
        }
    }
}