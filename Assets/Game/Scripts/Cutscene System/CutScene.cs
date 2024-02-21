using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.CutsceneSystem {
    public class CutScene : MonoBehaviour {
        [Serializable]
        public struct Audio {
            public AudioClip audioClip;
            public bool playNextImage;
            public int coolDownInSeconds;
        }

        [SerializeField] private CutSceneSO cutSceneSO;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Transform imageContainer;

        private List<Image> imagePrefabs = new();
        private float audioCooldown;
        private int currentDisplayedImage = 0;
        private int currentAudioIndex = 0;
        private bool isStarted = false;
        private bool isFinished = false;
        private bool isInCooldown = false;
        public Action CutSceneStartAction;
        public Action CutSceneEndAction;
        public void StartCutScene() {
            if (!isFinished) {
                CutSceneStartAction?.Invoke();
                for (int i = 0; i < cutSceneSO.imageList.Count; i++) {
                    Image image = Instantiate(cutSceneSO.imageList[i], imageContainer);
                    imagePrefabs.Add(image);
                    image.gameObject.SetActive(false);
                }
                currentDisplayedImage = 0;
                currentAudioIndex = 0;
                imagePrefabs[currentDisplayedImage].gameObject.SetActive(true);
                audioSource.clip = cutSceneSO.audioList[currentAudioIndex].audioClip;
                audioSource.Play();
                isStarted = true;
            }
        }

        public void SkipCutscene() {
            if (isStarted) {
                audioSource.Stop();
                isStarted = false;
                isFinished = true;
                CutSceneEndAction?.Invoke();
                foreach (var image in imagePrefabs) {
                    Destroy(image.gameObject);
                }
            }
        }

        private void PlayNextImage() {
            if (cutSceneSO.audioList[currentAudioIndex].playNextImage) {   
                imagePrefabs[currentDisplayedImage].gameObject.SetActive(false);
                currentDisplayedImage++;
                if (currentDisplayedImage >= imagePrefabs.Count) {
                    return;
                }
                imagePrefabs[currentDisplayedImage].gameObject.SetActive(true);
            }
        }
        private void Update() {
            if (isStarted) {
                if (!audioSource.isPlaying && !isInCooldown) {
                    StartCoroutine(AudioCooldown(cutSceneSO.audioList[currentAudioIndex].coolDownInSeconds));
            
                    PlayNextImage();
                    
                    currentAudioIndex++;
                    if (currentAudioIndex >= cutSceneSO.audioList.Count) {
                        isFinished = true;
                        isStarted = false;
                        CutSceneEndAction?.Invoke();
                    } else {
                        audioSource.clip = cutSceneSO.audioList[currentAudioIndex].audioClip;
                        audioSource.Play();
                    }
                } 
            }
        }

        private IEnumerator AudioCooldown(int cooldownSeconds) {
            isInCooldown = true;
            yield return new WaitForSeconds(cooldownSeconds);
            isInCooldown = false;
        }
    }
}