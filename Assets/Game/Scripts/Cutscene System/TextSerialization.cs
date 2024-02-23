using System;
using System.Collections;
using TMPro;
using UnityEngine;


namespace Game.Scripts.CutsceneSystem
{
    public class TextSerialization : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshProGUI;
        [SerializeField] private float delayBetweenCharacters = 0.05f;
        private CutScene cutScene;
        private int currentWordIndex = 0;
        private int currentAudioIndex = 0;
        
        private void Start() {
            this.cutScene = GetComponent<CutScene>();
            cutScene.DisplayedTextEvent += SerializeCurrent;
        }
        public void SerializeCurrent(int currentAudioIndex) {
            this.currentAudioIndex = currentAudioIndex;
            if (currentWordIndex >= cutScene.getCutSceneSO().audioList[currentAudioIndex].AudioText.Count) {
                currentWordIndex = 0;
                return;
            }
            textMeshProGUI.text = cutScene.getCutSceneSO().audioList[currentAudioIndex].AudioText[currentWordIndex];
            StartCoroutine(SerializationCoroutine());
        }
        private IEnumerator SerializationCoroutine() {
            int totalVisibleCharactersCount = textMeshProGUI.textInfo.characterCount;
            int count = 0;
            while (true) {
                int visibleCharactersCount = count % (totalVisibleCharactersCount + 1);
                textMeshProGUI.maxVisibleCharacters = visibleCharactersCount;

                if (visibleCharactersCount >= totalVisibleCharactersCount) {
                    currentWordIndex++;
                    yield return new WaitForSeconds(1f);
                    SerializeCurrent(currentAudioIndex);
                    break;
                }
                count++;
                yield return new WaitForSeconds(delayBetweenCharacters);
            }
        }
    }
}
