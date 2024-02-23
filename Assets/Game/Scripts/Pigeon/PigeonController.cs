using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PigeonProject.Manager;

namespace PigeonProject.Pigeon
{
    public class PigeonController : MonoBehaviour
    {
        [SerializeField] private float timer;
        [SerializeField] Flight _flight;

        public void SetCustomSpeed(float speed){
            //adding custom speed for tempry time //w
        }

        void OnCollisionEnter(Collision other)
        {
            GameManager.Instance.DeathScreen();
        }
    }
}
