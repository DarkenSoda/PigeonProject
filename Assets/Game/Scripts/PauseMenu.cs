using PigeonProject.Consts;
using PigeonProject.Loading;
using PigeonProject.Manager;
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
            Time.timeScale = 1;
            
            PlayerPrefs.SetInt(Const.CHECKPOINT , GameManager.Instance.CurrentCheckpoint);
            LoadingManager.Instance.LoadScene(0);
        }
        public void Apply()
        {
            pauseMenu.SetActive(true);
            optionMenue.SetActive(false);
        }
    }
}
