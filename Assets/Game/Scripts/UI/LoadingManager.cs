using System.Collections;
using System.Collections.Generic;
using PigeonProject.Patterns;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PigeonProject.Loading
{
    public class LoadingManager : Singleton<LoadingManager>
    {   
        [SerializeField] private GameObject _loadingScreen;

        [SerializeField] private GameObject _spinner;
        [SerializeField , Range(5, 1000f)] private float _speed;

        public void LoadScene(string id){
            if(_loadingScreen != null)_loadingScreen.SetActive(true);
            StartCoroutine(SwitchSceneAsync(id));
        }
        public void LoadScene(int id){
            if(_loadingScreen != null)_loadingScreen.SetActive(true);
            StartCoroutine(SwitchSceneAsync(id));
        }

        IEnumerator SwitchSceneAsync(int id){
            AsyncOperation asyncload = SceneManager.LoadSceneAsync(id);
            while(!asyncload.isDone){
                yield return null;
            }
            yield return new WaitForSeconds(0.3f);
            if(_loadingScreen != null)_loadingScreen.SetActive(false);
        }
        IEnumerator SwitchSceneAsync(string id){
            AsyncOperation asyncload = SceneManager.LoadSceneAsync(id);
            while(!asyncload.isDone){
                yield return null;
            }
            yield return new WaitForSeconds(0.3f);
            if(_loadingScreen != null)_loadingScreen.SetActive(false);
        }
        void Update()
        {
            if(_loadingScreen == null) return;
            _spinner.transform.eulerAngles += new Vector3(0, 0 , Time.deltaTime * _speed);
        }
    }
}
