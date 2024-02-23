using UnityEngine;
using PigeonProject.Patterns;
using PigeonProject.Consts;
using System.Collections.Generic;
using PigeonProject.Checkpoints;
using System.Collections;
using TMPro;
using PigeonProject.Loading;
using Game.Scripts.CutsceneSystem;




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

        [SerializeField] GameObject titlePanel;
        [SerializeField] TMP_Text _act_title_text;
        [SerializeField] TMP_Text _act_subtitle_text;


        void Start()
        {
            CurrentCheckpoint = PlayerPrefs.GetInt(Const.CHECKPOINT);

            if(CurrentCheckpoint == 0){
                var pointofIntrest = Checkpoints[0].GetComponentInParent<PointOfInterest>();
                pointofIntrest.Initialize();
                return;
            }else{
                
            }

            SetPlayer(CurrentCheckpoint);
        }

        public void SetTitle(string _title, string _subtitle){
            _act_title_text.text = _title;
            _act_subtitle_text.text = _subtitle;
            StartCoroutine(TitlePanel());
        }
        public void EndGame(){
            LoadingManager.Instance.LoadScene("CreditsScene");
        }
        public void SaveCheckpoint(int _index){
            CurrentCheckpoint = _index;

            PlayerPrefs.SetInt(Const.CHECKPOINT, CurrentCheckpoint);
            StartCoroutine(TimedSavePanel());
        }
        IEnumerator TitlePanel(){
            titlePanel.SetActive(true);
            yield return new WaitForSeconds(3f);
            titlePanel.SetActive(false);
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
                    var pointofIntrest = Checkpoints[index].GetComponentInParent<PointOfInterest>();
                    pointofIntrest.Initialize();

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
