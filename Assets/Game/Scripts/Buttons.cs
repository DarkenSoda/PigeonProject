using System.Collections;
using System.Collections.Generic;
using PigeonProject.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PigeonProject
{
    public class Buttons : MonoBehaviour
    {
        [SerializeField] private Animator _mainMenuAnimator;
        [SerializeField] private GameObject mainMenu, settingMenu;

        void Start()
        {
            StartCoroutine(StartAnimation());
        }

        IEnumerator StartAnimation()
        {
            _mainMenuAnimator.SetBool("FadeIn" , true);
            yield return new WaitForSeconds(1);
            _mainMenuAnimator.SetBool("FadeIn" , false);
        }
        public void StartGame()
        {
            LoadingManager.Instance.LoadScene(1);
        }
        public void Options()
        {
            mainMenu.SetActive(false);
            settingMenu.SetActive(true);
        }
        public void Apply()
        {
            mainMenu.SetActive(true);
            settingMenu.SetActive(false);
        }
        public void Credits()
        {
            LoadingManager.Instance.LoadScene("CreditsScene");
        }
        public void ArtBook()
        {
             
        }
        public void Quit()
        {
            Application.Quit();
        }

    }
}
