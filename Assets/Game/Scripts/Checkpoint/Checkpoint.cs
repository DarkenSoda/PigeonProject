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

        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player")){
                GameManager.Instance.SaveCheckpoint(_index);
            }
        }
    
    }
}
