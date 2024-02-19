using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PigeonProject
{
    public class Buttons : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu, settingMenu;
        public void Continue()
        {

        }
       public void NewGame()
        {
            SceneManager.LoadSceneAsync(1);
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
