using UnityEngine;
using PigeonProject.Patterns;
using PigeonProject.Consts;
using System.Collections.Generic;
using PigeonProject.Checkpoints;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace PigeonProject.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        public int CurrentCheckpoint = 0;
        [SerializeField] private GameObject _deathScreen;
        [SerializeField] List<Checkpoint> Checkpoints = new();
        public GameObject Player;
        [Header("UI")]
        [SerializeField] GameObject SavePanel;

        void Start()
        {
            CurrentCheckpoint = PlayerPrefs.GetInt(Const.CHECKPOINT);

            if(CurrentCheckpoint == 0) return;

            SetPlayer(CurrentCheckpoint);
        }
        public void SaveCheckpoint(int _index){
            CurrentCheckpoint = _index;

            PlayerPrefs.SetInt(Const.CHECKPOINT, CurrentCheckpoint);
            StartCoroutine(TimedSavePanel());
        }

        IEnumerator TimedSavePanel(){
            SavePanel.SetActive(true);
            yield return new WaitForSeconds(2f);
            SavePanel.SetActive(false);
        }
        public void SetPlayer(int _index){
            Player.GetComponent<CharacterController>().enabled = false;
            int index = 0;
            for (int i = 0; i < Checkpoints.Count; i++)
            {
                if(Checkpoints[i]._index == _index){
                    index = i;
                    Player.transform.position = Checkpoints[index]._startPosition.position;
                    Player.GetComponent<CharacterController>().enabled = true;
                    Debug.Log("Start position at Checkpoint " + Checkpoints[index]._index);
                    return;
                }
            }
        }
        public void DeathScreen(){
            _deathScreen.SetActive(true);
        }
    } 


    #if UNITY_EDITOR
    [CustomEditor(typeof(GameManager))]
    public class GameManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(GUILayout.Button("Clear Entry")){
                PlayerPrefs.DeleteAll();
            }
        }
    }
    #endif
}
