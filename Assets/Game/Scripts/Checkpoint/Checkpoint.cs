using System.Collections;
using System.Collections.Generic;
using PigeonProject.Manager;
using UnityEngine;

namespace PigeonProject.Checkpoints
{
    public class Checkpoint : MonoBehaviour
    {
        public int _index;
        public Transform _startPosition;

        [SerializeField] private string _act_title;
        [SerializeField, TextArea(0, 3)] private string _act_subtitle;


        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player")){
                GameManager.Instance.SetTitle(_act_title, _act_subtitle);
                GameManager.Instance.SaveCheckpoint(_index);
            }
        }
    
    }
}
