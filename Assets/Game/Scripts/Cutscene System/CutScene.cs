using System;
using System.Collections;
using System.Collections.Generic;
using PigeonProject.Inputs;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.CutsceneSystem {
    public class CutScene : MonoBehaviour {
        [Serializable]
        public struct Audio {
            public AudioClip audioClip;
            public bool playNextImage;
            public int coolDownInSeconds;
            public bool isLastAudio;
        }

        [SerializeField] private CutSceneSO cutSceneSO;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Transform imageContainer;
        [SerializeField] private Animator UIanimator;
        private const string CUTSCENE_TRIGGER = "CutSceneUITransition";
        private const string ICUTSCENE_TRIGGER = "InverseCutSceneUITransition"; 
        private List<Image> imagePrefabs = new();
        private float audioCooldown;
        private int currentDisplayedImage = 0;
        private int currentAudioIndex = 0;
        private bool isStarted = false;
        private bool isFinished = false;
        private bool isInCooldown = false;
        public Action CutSceneStartAction;
        public Action CutSceneEndAction;
        
        private void Start() {
            Debug.Log(GameInput.Singleton);
            GameInput.Singleton.OnCutsceneSkip += SkipCutscene;    
        }
        public void StartCutScene() {
            if (!isFinished) {
                CutSceneStartAction?.Invoke();
                for (int i = 0; i < cutSceneSO.imageList.Count; i++) {
                    Image image = Instantiate(cutSceneSO.imageList[i], imageContainer);
                    image.transform.localPosition = new Vector3(image.transform.localPosition.x, image.transform.localPosition.y, -10f);
                    imagePrefabs.Add(image);
                    image.gameObject.SetActive(false);
                }
                currentDisplayedImage = 0;
                currentAudioIndex = 0;
                imagePrefabs[currentDisplayedImage].gameObject.SetActive(true);
                UIanimator.SetTrigger(CUTSCENE_TRIGGER);
                StartCoroutine(WaitTheStartAnimationsEnd());

            }
        }

        public void SkipCutscene() {
            if (isStarted) {
                audioSource.Stop();
                EndCutScene();
            }
        }

        private void EndCutScene() {
            isFinished = true;
            isStarted = false;
            UIanimator.SetTrigger(ICUTSCENE_TRIGGER);
            StartCoroutine(WaitTheEndAnimationsEnd());
            CutSceneEndAction?.Invoke();
        }

        private void PlayNextImage() {
            if (cutSceneSO.audioList[currentAudioIndex].playNextImage && !cutSceneSO.audioList[currentAudioIndex].isLastAudio) {   
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
                        EndCutScene();
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

        private IEnumerator WaitTheStartAnimationsEnd() {
            Debug.Log("waiting for the animation to end");
            yield return new WaitForSeconds(1.3f);
            Debug.Log("animation has ended");

            audioSource.clip = cutSceneSO.audioList[currentAudioIndex].audioClip;
            audioSource.Play();
            isStarted = true;
        }

        private IEnumerator WaitTheEndAnimationsEnd() {
            Debug.Log("waiting for the animation to end");
            yield return new WaitForSeconds(2f);
            Debug.Log("animation has ended");
            foreach (var image in imagePrefabs) {
                Destroy(image.gameObject);
            }
        }
    }
}