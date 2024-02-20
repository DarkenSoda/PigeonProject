using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace PigeonProject
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu,optionMenue;
        private bool isPaused = false;
        private void Update()
        {
            pauseMenu.SetActive(isPaused);
            if(Input.GetKeyDown(KeyCode.Escape) && !isPaused)
            {
                Time.timeScale = 0;
                isPaused = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
            }
        }
        public void Resume()
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        public void Options()
        {
            optionMenue.SetActive(true);
        }
        public void SaveAndExit()
        {
            
        }
        public void Apply()
        {
            pauseMenu.SetActive(true);
            optionMenue.SetActive(false);
        }
    }
}
